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
    public class HospitalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HospitalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalForUser>>> GetHospitals()
        {
            List<Hospital> hospitals = await _context.Hospitals.Include(d => d.Doctors).ToListAsync();

            List<HospitalForUser> hospitalsForUser = new List<HospitalForUser>();

            foreach (Hospital h in hospitals)
            {
                List<DoctorsForUser> doctorsForUser = new List<DoctorsForUser>();

                foreach (Doctor d in h.Doctors)
                {
                    DoctorsForUser tempDoctor = new DoctorsForUser()
                    {
                        Email = d.Email,
                        Name = d.Name,
                        Speciality = d.Speciality
                    };

                    doctorsForUser.Add(tempDoctor);
                }

                HospitalForUser tempHospital = new HospitalForUser()
                {
                    Name = h.Name,
                    Doctors = doctorsForUser
                };

                hospitalsForUser.Add(tempHospital);
            }

            return hospitalsForUser;
        }

        // POST: api/Symptoms
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospital(Hospital hospital)
        {
            _context.Hospitals.Add(hospital);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HospitalExists(hospital.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoctor", new { id = hospital.Name }, hospital);
        }

        private bool HospitalExists(string id)
        {
            return _context.Symptoms.Any(e => e.Name == id);
        }
    }
}
