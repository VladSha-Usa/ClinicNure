using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalApp.Data;
using FinalApp.Models;

namespace FinalApp.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RequestForUser>>> GetRequests(string id)
        {
            List<Request> requests = await _context.Requests.Include(s => s.Symptoms)
                                                            .Include(p => p.Patient)
                                                            .Include(h => h.Hospital).ThenInclude(d => d.Doctors)
                                                            .Include(d => d.Doctor)
                                                            .Include(d => d.Disease).Where(i => i.Patient.Email.Contains(id)).ToListAsync();

            List<RequestForUser> requestsForUser = new List<RequestForUser>();

            foreach (Request r in requests)
            {
                List<DoctorsForUser> tempDoctors = new List<DoctorsForUser>();
                foreach (Doctor d in r.Hospital.Doctors)
                {
                    DoctorsForUser temp = new DoctorsForUser()
                    {
                        Email = d.Email,
                        Name = d.Name,
                        Speciality = d.Speciality
                    };

                    tempDoctors.Add(temp);
                }

                RequestForUser tempRequest = new RequestForUser()
                {
                    Id = r.Id,
                    Date = r.Date,
                    Symptoms = r.Symptoms,
                    Patient = r.Patient,
                    Hospital = new HospitalForUser()
                    {
                        Name = r.Hospital.Name,
                        Doctors = tempDoctors
                    },
                    Doctor = new DoctorsForUser()
                    {
                        Email = r.Doctor.Email,
                        Name = r.Doctor.Name,
                        Speciality = r.Doctor.Speciality
                    },
                    Status = r.Status,
                    Disease = r.Disease
                };

                requestsForUser.Add(tempRequest);
            }

            return Ok(requestsForUser);
        }

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(RequestForUser requestForUser)
        {
            Hospital hospital = _context.Hospitals.Find(requestForUser.Hospital.Name);
            Doctor doctor = _context.Doctors.Find(requestForUser.Doctor.Email);
            Patient patient = _context.Patients.Find(requestForUser.Patient.Email);

            if (patient == null)
            {
                patient = _context.Patients.Find("fb:" + requestForUser.Patient.Email);
            }

            if (patient == null) 
            {
                patient = _context.Patients.Find("g:" + requestForUser.Patient.Email);
            }

            List<Symptom> symptoms = new List<Symptom>();
            foreach (Symptom s in requestForUser.Symptoms)
            {
                symptoms.Add(_context.Symptoms.Find(s.Name));
            }

            Request request = new Request()
            {
                Id = default(int),
                Date = requestForUser.Date,
                Symptoms = symptoms,
                Patient = patient,
                Hospital = hospital,
                Doctor = doctor,
                Status = "Очікує обробку",
                Disease = null
            };

            _context.Requests.Add(request);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RequestExists(request.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(request);
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
