using Blazorcrud.Server.Authorization;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blazorcrud.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// Returns a list of paginated people with a default page size of 5.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetPeople([FromQuery] string? name, int page)
        {
            return Ok(_personRepository.GetPeople(name, page));
        }

        /// <summary>
        /// Gets a specific person by Id.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPerson(int id)
        {
            return Ok(await _personRepository.GetPerson(id));
        }

        /// <summary>
        /// Creates a person with child addresses.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddPerson(Person person)
        {
            return Ok(await _personRepository.AddPerson(person));
        }
        
        /// <summary>
        /// Updates a person with a specific Id.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> UpdatePerson(Person person)
        {
            return Ok(await _personRepository.UpdatePerson(person));
        }

        /// <summary>
        /// Deletes a person with a specific Id.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            return Ok(await _personRepository.DeletePerson(id));
        }
    }
}
