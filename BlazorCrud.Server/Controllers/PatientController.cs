using BlazorCrud.Shared.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BlazorCrud.Shared.Models;

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

        /// <summary>
        /// Returns a list of paginated patients with a default page size of 5.
        /// </summary>
        [HttpGet]
        public PagedResult<Patient> GetAll([FromQuery]int page)
        {
            int pageSize = 5;
            return _context.Patients
                .OrderBy(p => p.Id)
                .GetPaged(page, pageSize);
        }

        /// <summary>
        /// Gets a specific patient by Id.
        /// </summary>
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

        /// <summary>
        /// Creates a patient.
        /// </summary>
        [HttpPost]
        [Authorize]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return CreatedAtRoute("GetPatient", new { id = patient.Id }, patient);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Updates a patient with a specific Id.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, Patient patient)
        {
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Deletes a specific patient by Id.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
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
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}