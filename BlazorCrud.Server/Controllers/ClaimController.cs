using AutoMapper;
using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using BlazorCrud.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BlazorCrud.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        /// Returns a list of paginated claims with a default page size of 10.
        /// </summary>
        [HttpGet]
        public PagedResult<Claim> GetAll([FromQuery]string name, int page)
        {
            int pageSize = 10;
            if (name != null)
            {
                return _context.Claims
                .Where(c => c.Patient.Contains(name, System.StringComparison.CurrentCultureIgnoreCase) ||
                       c.Organization.Contains(name, System.StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(c => c.Id)
                .AsNoTracking()
                .GetPaged(page, pageSize);
            }
            else
            {
                return _context.Claims
                  .OrderBy(c => c.Id)
                  .GetPaged(page, pageSize);
            }
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
            item = _context.Claims
                .Include(claim => claim.LineItems)
                .Single(c => c.Id == id);
            return item;
        }

        /// <summary>
        /// Creates a claim.
        /// </summary>
        [HttpPost]
        [Authorize]
        public IActionResult Create(ClaimViewModel claim)
        {
            // Map to ViewModel - Replace with Automapper
            Claim _claim = new Claim();
            _claim.LineItems = new System.Collections.Generic.List<Shared.Models.LineItem>();
            _claim.Patient = claim.SelectedPatient.Name;
            _claim.Organization = claim.SelectedOrganization.Name;
            _claim.Type = claim.Type;
            _claim.Status = claim.Status;
            foreach (Shared.ViewModels.LineItem li in claim.LineItems)
            {
                Shared.Models.LineItem _li = new Shared.Models.LineItem();
                _li.Service = li.Service;
                _li.Amount = li.Amount;
                _claim.LineItems.Add(_li);
            }

            if (ModelState.IsValid)
            {
                _claim.ModifiedDate = DateTime.Now;
                _context.Claims.Add(_claim);
                _context.SaveChanges();
                return CreatedAtRoute("GetClaim", new { id = _claim.Id }, _claim);
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
        public IActionResult Update(int id, ClaimViewModel claim)
        {
            // Map to ViewModel - Replace with Automapper
            Claim _claim = new Claim();
            _claim.LineItems = new System.Collections.Generic.List<Shared.Models.LineItem>();
            _claim.Id = claim.Id;
            _claim.Patient = claim.SelectedPatient.Name;
            _claim.Organization = claim.SelectedOrganization.Name;
            _claim.Type = claim.Type;
            _claim.Status = claim.Status;
            foreach (BlazorCrud.Shared.ViewModels.LineItem li in claim.LineItems)
            {
                BlazorCrud.Shared.Models.LineItem _li = new Shared.Models.LineItem();
                _li.Id = li.Id;
                _li.Service = li.Service;
                _li.Amount = li.Amount;
                _claim.LineItems.Add(_li);
            }

            if (ModelState.IsValid)
            {
                var existingClaim = _context.Claims
                    .Include(c => c.LineItems)
                    .Single(c => c.Id == id);
                if (existingClaim == null)
                {
                    return NotFound();
                }

                // Update Existing Claim
                existingClaim.ModifiedDate = DateTime.Now;
                _context.Entry(existingClaim).CurrentValues.SetValues(claim) ;

                // Delete Line Items
                foreach (var existingLineItem in existingClaim.LineItems.ToList())
                {
                    if (!claim.LineItems.Any(c => c.Id == existingLineItem.Id))
                        _context.LineItems.Remove(existingLineItem);
                }

                // Update and Insert Line Items
                foreach (var lineItemModel in claim.LineItems)
                {
                    var existingLineItem = existingClaim.LineItems
                        .Where(c => c.Id == lineItemModel.Id)
                        .SingleOrDefault();
                    if (existingLineItem != null)
                        _context.Entry(existingLineItem).CurrentValues.SetValues(lineItemModel);
                    else
                    {
                        var newLineItem = new Shared.Models.LineItem
                        {
                            Id = lineItemModel.Id,
                            Service = lineItemModel.Service,
                            Amount = lineItemModel.Amount
                        };
                        existingClaim.LineItems.Add(newLineItem);
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