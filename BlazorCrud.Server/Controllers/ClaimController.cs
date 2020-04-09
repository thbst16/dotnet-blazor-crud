using AutoMapper;
using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
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
        public IActionResult Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.ModifiedDate = DateTime.Now;
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
                        var newLineItem = new LineItem
                        {
                            Id = lineItemModel.Id,
                            Service = lineItemModel.Service,
                            Provider = lineItemModel.Provider,
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




            if (ModelState.IsValid)
            {
                var cla = _context.Claims.Find(id);
                if (cla == null)
                {
                    return NotFound();
                }

                claim.ModifiedDate = DateTime.Now;
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