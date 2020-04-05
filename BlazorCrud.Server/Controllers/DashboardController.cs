using BlazorCrud.Shared.Data;
using BlazorCrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .AsNoTracking()
                .ToDictionary(k => k.state, i => Convert.ToDouble(i.count));

            dashboard.OrganizationsByType = _organizationContext.Organizations
                .GroupBy(o => o.Type)
                .Select(g => new { type = g.Key, count = g.Count() })
                .AsNoTracking()
                .ToDictionary(k => k.type, i => Convert.ToDouble(i.count));

            dashboard.ClaimsByType = _claimContext.Claims
                .GroupBy(c => c.Type)
                .Select(g => new { type = g.Key, count = g.Count() })
                .AsNoTracking()
                .ToDictionary(k => k.type, i => Convert.ToDouble(i.count));

            dashboard.UpdatedEntitiesByDate = CreateDashboardDataArray();

            return dashboard;
        }        

        private Dictionary<string, Dictionary<string, double>> CreateDashboardDataArray()
        {
            Dashboard dashboard = new Dashboard();

            // Select data from database by entities over ranges
            var patientQuery = _patientContext.Patients
                .GroupBy(p => p.ModifiedDate.Date)
                .Select(g => new { date = g.Key, counter = g.Count() })
                .AsNoTracking()
                .ToDictionary(k => k.date, i => i.counter);

            var organizationQuery = _organizationContext.Organizations
                .GroupBy(o => o.ModifiedDate.Date)
                .Select(g => new { date = g.Key, counter = g.Count() })
                .AsNoTracking()
                .ToDictionary(k => k.date, i => i.counter);

            var claimQuery = _claimContext.Claims
                .GroupBy(c => c.ModifiedDate.Date)
                .Select(g => new { date = g.Key, counter = g.Count() })
                .AsNoTracking()
                .ToDictionary(k => k.date, i => i.counter);

            // Augment query values with actual dates
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

            // Associate date / value pairs back into Dictionary data structure
            Dictionary<string, Dictionary<string, double>> UpdatedEntitiesCollection = new Dictionary<string, Dictionary<string, double>>
            {
                {
                    DateTime.Today.AddDays(-3).ToString("MM/dd/yyyy"),
                    new Dictionary<string, double> {
                        { "Patients", Convert.ToDouble(patientMinus3) },
                        { "Organizations", Convert.ToDouble(organizationMinus3) },
                        { "Claims", Convert.ToDouble(claimMinus3)}
                    }
                },
                {
                    DateTime.Today.AddDays(-2).ToString("MM/dd/yyyy"),
                    new Dictionary<string, double> {
                        { "Patients", Convert.ToDouble(patientMinus2) },
                        { "Organizations", Convert.ToDouble(organizationMinus2) },
                        { "Claims", Convert.ToDouble(claimMinus2)}
                    }
                },
                {
                    DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"),
                    new Dictionary<string, double> {
                        { "Patients", Convert.ToDouble(patientMinus1) },
                        { "Organizations", Convert.ToDouble(organizationMinus1) },
                        { "Claims", Convert.ToDouble(claimMinus1)}
                    }
                },
                {
                    DateTime.Today.ToString("MM/dd/yyyy"),
                    new Dictionary<string, double> {
                        { "Patients", Convert.ToDouble(patientToday) },
                        { "Organizations", Convert.ToDouble(organizationToday) },
                        { "Claims", Convert.ToDouble(claimToday) }
                    }
                }
            };

            return UpdatedEntitiesCollection;
        }
    }
}