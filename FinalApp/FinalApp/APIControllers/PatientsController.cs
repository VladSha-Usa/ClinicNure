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
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetHospitals()
        {
            return await _context.Patients.ToListAsync();
        }

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            Patient find = patient;
            if (patient.IsRegistration)
            {
                _context.Patients.Add(patient);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (PatientExists(patient.Email))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                find = _context.Patients.Find(patient.Email);
                if (find == null)
                {
                    return NotFound();
                }
                else if (!patient.Email.StartsWith("fb:") && !patient.Email.StartsWith("g:"))
                {
                    if (!patient.Password.Equals(find.Password))
                    {
                        return NotFound();
                    }
                }
            }

            return Ok(find);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(string id)
        {
            return _context.Patients.Any(e => e.Email == id);
        }
    }
}
