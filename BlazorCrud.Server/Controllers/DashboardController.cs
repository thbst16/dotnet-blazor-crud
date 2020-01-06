using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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

            var patientQuery = _patientContext.Patients
                .GroupBy(p => p.ModifiedDate.Date)
                .Select(g => new { date = g.Key, counter = g.Count() })
                .ToDictionary(k => k.date, i => i.counter);

            var organizationQuery = _organizationContext.Organizations
                .GroupBy(o => o.ModifiedDate.Date)
                .Select(g => new { date = g.Key, counter = g.Count() })
                .ToDictionary(k => k.date, i => i.counter);

            var claimQuery = _claimContext.Claims
                .GroupBy(c => c.ModifiedDate.Date)
                .Select(g => new { date = g.Key, counter = g.Count() })
                .ToDictionary(k => k.date, i => i.counter);

            int patientMinus3;
            patientQuery.TryGetValue(DateTime.Today.AddDays(-3), out patientMinus3);
            int organizationMinus3;
            organizationQuery.TryGetValue(DateTime.Today.AddDays(-3), out organizationMinus3);
            int claimMinus3;
            claimQuery.TryGetValue(DateTime.Today.AddDays(-3), out claimMinus3);
            int patientMinus2;
            patientQuery.TryGetValue(DateTime.Today.AddDays(-2), out patientMinus2);
            int organizationMinus2;
            organizationQuery.TryGetValue(DateTime.Today.AddDays(-2), out organizationMinus2);
            int claimMinus2;
            claimQuery.TryGetValue(DateTime.Today.AddDays(-2), out claimMinus2);
            int patientMinus1;
            patientQuery.TryGetValue(DateTime.Today.AddDays(-1), out patientMinus1);
            int organizationMinus1;
            organizationQuery.TryGetValue(DateTime.Today.AddDays(-1), out organizationMinus1);
            int claimMinus1;
            claimQuery.TryGetValue(DateTime.Today.AddDays(-1), out claimMinus1);
            int patientToday;
            patientQuery.TryGetValue(DateTime.Today, out patientToday);
            int organizationToday;
            organizationQuery.TryGetValue(DateTime.Today, out organizationToday);
            int claimToday;
            claimQuery.TryGetValue(DateTime.Today, out claimToday);

            dashboard.UpdatedEntitiesByDate = new Dictionary<string, Dictionary<string, int>>
            {
                { 
                    DateTime.Today.AddDays(-3).ToString(), 
                    new Dictionary<string, int> { 
                        { "Patients", patientMinus3 }, 
                        { "Organizations", organizationMinus3 }, 
                        { "Claims", claimMinus3} 
                    } 
                },
                { 
                    DateTime.Today.AddDays(-2).ToString(), 
                    new Dictionary<string, int> { 
                        { "Patients", patientMinus2 }, 
                        { "Organizations", organizationMinus2 }, 
                        { "Claims", claimMinus2} 
                    } 
                },
                { 
                    DateTime.Today.AddDays(-1).ToString(), 
                    new Dictionary<string, int> { 
                        { "Patients", patientMinus1 }, 
                        { "Organizations", organizationMinus1 }, 
                        { "Claims", claimMinus1} 
                    } 
                },
                { 
                    DateTime.Today.ToString(), 
                    new Dictionary<string, int> { 
                        { "Patients", patientToday }, 
                        { "Organizations", organizationToday }, 
                        { "Claims",claimToday} 
                    } 
                }
            };
            return dashboard;
        }
    }
}