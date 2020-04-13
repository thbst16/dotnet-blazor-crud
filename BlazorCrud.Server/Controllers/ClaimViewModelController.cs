using System;
using System.Linq;
using AutoMapper;
using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimViewModelController : ControllerBase
    {
        private readonly ClaimContext _claimContext;
        private readonly PatientContext _patientContext;
        private readonly OrganizationContext _organizationContext;
        private readonly IMapper _mapper;

        public ClaimViewModelController(ClaimContext claimContext, PatientContext patientContext, OrganizationContext organizationContext, IMapper mapper)
        {
            _claimContext = claimContext;
            _patientContext = patientContext;
            _organizationContext = organizationContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets claim view-model for update transation
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<ClaimViewModel> GetClaimModel(int id)
        {
            var item = _claimContext.Claims.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            item = _claimContext.Claims
                .Include(claim => claim.LineItems)
                .Single(c => c.Id == id);

            // Map to ViewModel - Replace with AutoMapper
            // No current integrity between domain models, hence random numbers
            Random random = new Random();
            ClaimViewModel _claim = new ClaimViewModel();
            _claim.LineItems = new System.Collections.Generic.List<Shared.ViewModels.LineItem>();
            _claim.Id = item.Id;
            _claim.SelectedPatient = _patientContext.Patients.Find(random.Next(1,100));
            _claim.SelectedOrganization = _organizationContext.Organizations.Find(random.Next(1, 100));
            _claim.Type = item.Type;
            _claim.Status = item.Status;
            foreach (Shared.Models.LineItem li in item.LineItems)
            {
                Shared.ViewModels.LineItem _li = new Shared.ViewModels.LineItem();
                _li.Id = li.Id;
                _li.Service = li.Service;
                _li.Amount = li.Amount;
                _claim.LineItems.Add(_li);
            }

            return _claim;
        }
    }
}