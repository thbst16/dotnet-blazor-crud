using AutoMapper;
using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                .AsNoTracking()
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
        /// Returns top 10 results for type-ahead UI function
        /// </summary>
        [HttpGet]
        [Route("TypeAhead")]
        public IEnumerable<Organization> TypeAhead([FromQuery]string name)
        {
            if (name != null)
            {
                return _context.Organizations
                .Where(p => p.Name.Contains(name, System.StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(p => p.Id)
                .Take(10)
                .AsNoTracking();
            }
            else
            {
                return _context.Organizations
                  .OrderBy(p => p.Id)
                  .Take(10)
                  .AsNoTracking();
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
            item = _context.Organizations
                .Include(organization => organization.Addresses)
                .Single(o => o.Id == id);
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
                var existingOrganization = _context.Organizations
                    .Include(or => or.Addresses)
                    .Single(or => or.Id == id);
                if (existingOrganization == null)
                {
                    return NotFound();
                }

                // Update Existing Organization
                existingOrganization.ModifiedDate = DateTime.Now;
                _context.Entry(existingOrganization).CurrentValues.SetValues(organization);

                // Delete Addresses
                foreach (var existingAddress in existingOrganization.Addresses.ToList())
                {
                    if (!organization.Addresses.Any(o => o.Id == existingAddress.Id))
                        _context.Addresses.Remove(existingAddress);
                }

                // Update and Insert Addresses
                foreach (var addressModel in organization.Addresses)
                {
                    var existingAddress = existingOrganization.Addresses
                        .Where(a => a.Id == addressModel.Id)
                        .SingleOrDefault();
                    if (existingAddress != null)
                        _context.Entry(existingAddress).CurrentValues.SetValues(addressModel);
                    else
                    {
                        var newAddress = new Address
                        {
                            Id = addressModel.Id,
                            Street = addressModel.Street,
                            City = addressModel.City,
                            State = addressModel.State
                        };
                        existingOrganization.Addresses.Add(newAddress);
                    }
                }

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