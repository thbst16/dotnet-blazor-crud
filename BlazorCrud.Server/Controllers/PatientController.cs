using BlazorCrud.Server.DataAccess;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientContext _context;

        public PatientController(PatientContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Patient>> GetAll()
        {
            return _context.Patients.ToList();
        }

        [HttpGet("{id}", Name = "GetPatient")]
        public ActionResult<Patient> GetById(int id)
        {
            var item = _context.Patients.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();

            return CreatedAtRoute("GetPatient", new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Patient patient)
        {
            var pat = _context.Patients.Find(id);
            if (pat == null)
            {
                return NotFound();
            }

            // This is where you need AutoMapper
            pat.Name = patient.Name;
            pat.Gender = patient.Gender;
            pat.PrimaryCareProvider = patient.PrimaryCareProvider;
            pat.State = patient.State;

            _context.Patients.Update(pat);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return NoContent();
        }
    }
}