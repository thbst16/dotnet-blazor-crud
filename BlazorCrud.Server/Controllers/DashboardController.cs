using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlazorCrud.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly PatientContext _patientContext;
        private readonly OrganizationContext _organizationContext;
        private readonly ClaimContext _claimContext;

        public DashboardController(PatientContext patientContext, OrganizationContext organizationContext, ClaimContext claimContext)
        {
            _patientContext = patientContext;
            _organizationContext = organizationContext;
            _claimContext = claimContext;
        }

        /// <summary>
        /// Queries database for summary level details for dashboard.
        /// After initial query, pulls from cache to avoid performance penalty.
        /// </summary>
        [HttpGet]
        public Dashboard Get()
        {
            Dashboard dashboard = new Dashboard();

            dashboard.PatientsByState = _patientContext.Patients
                .GroupBy(p => p.State)
                .Select(g => new { state = g.Key, count = g.Count() })
                .ToDictionary(k => k.state, i => i.count);

            dashboard.OrganizationsByType = _organizationContext.Organizations
                .GroupBy(o => o.Type)
                .Select(g => new { type = g.Key, count = g.Count() })
                .ToDictionary(k => k.type, i => i.count);

            dashboard.ClaimsByType = _claimContext.Claims
                .GroupBy(c => c.Type)
                .Select(g => new { type = g.Key, count = g.Count() })
                .ToDictionary(k => k.type, i => i.count);

            var query = _patientContext.Patients
                .GroupBy(p => p.ModifiedDate.Date)
                .Select(g => new { date = g.Key, counter = g.Count() })
                .ToDictionary(k => k.date, i => i.counter);

            dashboard.UpdatedEntitiesByDate = new Dictionary<string, Dictionary<string, int>>
            {
                { "1/2/2020", new Dictionary<string, int> { { "Patients", 8 }, { "Organizations", 2 }, { "Claims", 15} } },
                { "1/3/2020", new Dictionary<string, int> { { "Patients", 5 }, { "Organizations", 3 }, { "Claims", 44} } },
                { "1/4/2020", new Dictionary<string, int> { { "Patients", 3 }, { "Organizations", 1 }, { "Claims", 22} } },
                { "1/5/2020", new Dictionary<string, int> { { "Patients", 9 }, { "Organizations", 2 }, { "Claims", 17} } }
            };
            return dashboard;
        }
    }
}