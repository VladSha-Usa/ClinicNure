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
    public class SymptomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SymptomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Symptoms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Symptom>>> GetSymptoms()
        {
            return await _context.Symptoms.ToListAsync();
        }

        // POST: api/Symptoms
        [HttpPost]
        public async Task<ActionResult<Symptom>> PostSymptom(Symptom symprom)
        {
            _context.Symptoms.Add(symprom);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SympromExists(symprom.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoctor", new { id = symprom.Name }, symprom);
        }

        private bool SympromExists(string id)
        {
            return _context.Symptoms.Any(e => e.Name == id);
        }
    }
}
