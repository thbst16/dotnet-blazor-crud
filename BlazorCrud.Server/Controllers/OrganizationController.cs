using AutoMapper;
using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BlazorCrud.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationContext _context;
        private readonly IMapper _mapper;

        public OrganizationController(OrganizationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns a list of paginated organizations with a default page size of 10.
        /// </summary>
        [HttpGet]
        public PagedResult<Organization> GetAll([FromQuery]string name, int page)
        {
            int pageSize = 10;
            if (name != null)
            {
                return _context.Organizations
                .Where(o => o.Name.Contains(name, System.StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(o => o.Id)
                .GetPaged(page, pageSize);
            }
            else
            {
                return _context.Organizations
                  .OrderBy(o => o.Id)
                  .GetPaged(page, pageSize);
            }
        }

        /// <summary>
        /// Gets a specific organization by Id.
        /// </summary>
        [HttpGet("{id}", Name = "GetOrganization")]
        public ActionResult<Organization> GetById(int id)
        {
            var item = _context.Organizations.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        /// <summary>
        /// Creates an organization.
        /// </summary>
        [HttpPost]
        [Authorize]
        public IActionResult Create(Organization organization)
        {
            if (ModelState.IsValid)
            {
                organization.ModifiedDate = DateTime.Now;
                _context.Organizations.Add(organization);
                _context.SaveChanges();
                return CreatedAtRoute("GetOrganization", new { id = organization.Id }, organization);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Updates an organization with a specific Id.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, Organization organization)
        {
            if (ModelState.IsValid)
            {
                var org = _context.Organizations.Find(id);
                if (org == null)
                {
                    return NotFound();
                }

                organization.ModifiedDate = DateTime.Now;
                _mapper.Map(organization, org);
                _context.Organizations.Update(org);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Deletes a specific organization by Id.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var organization = _context.Organizations.Find(id);
                if (organization == null)
                {
                    return NotFound();
                }

                _context.Organizations.Remove(organization);
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