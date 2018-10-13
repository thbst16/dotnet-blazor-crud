using BlazorCrud.Shared.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BlazorCrud.Shared.Models;
using AutoMapper;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly ClaimContext _context;
        private readonly IMapper _mapper;

        public ClaimController(ClaimContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of paginated claims with a default page size of 5.
        /// </summary>
        [HttpGet]
        public PagedResult<Claim> GetAll([FromQuery]int page)
        {
            int pageSize = 5;
            return _context.Claims
                .OrderBy(p => p.Id)
                .GetPaged(page, pageSize);
        }

        /// <summary>
        /// Gets a specific claim by Id.
        /// </summary>
        [HttpGet("{id}", Name = "GetClaim")]
        public ActionResult<Claim> GetById(int id)
        {
            var item = _context.Claims.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        /// <summary>
        /// Creates a claim.
        /// </summary>
        [HttpPost]
        [Authorize]
        public IActionResult Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Claims.Add(claim);
                _context.SaveChanges();
                return CreatedAtRoute("GetClaim", new { id = claim.Id }, claim);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Updates a claim with a specific Id.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, Claim claim)
        {
            if (ModelState.IsValid)
            {
                var cla = _context.Claims.Find(id);
                if (cla == null)
                {
                    return NotFound();
                }

                _mapper.Map(claim, cla);
                _context.Claims.Update(cla);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Deletes a specific claim by Id.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var claim = _context.Claims.Find(id);
                if (claim == null)
                {
                    return NotFound();
                }

                _context.Claims.Remove(claim);
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