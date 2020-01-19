using System;
using System.Collections.Generic;

namespace BlazorCrud.Shared.Models
{
    public class Dashboard
    {
        public Dictionary<string, double> PatientsByState { get; set; }
        public Dictionary<string, double> OrganizationsByType { get; set; }
        public Dictionary<string, double> ClaimsByType { get; set; }
        public Dictionary<string, Dictionary<string, double>> UpdatedEntitiesByDate { get; set; }

        public Dashboard()
        {
            PatientsByState = new Dictionary<string, double>();
            OrganizationsByType = new Dictionary<string, double>();
            ClaimsByType = new Dictionary<string, double>();
            UpdatedEntitiesByDate = new Dictionary<string, Dictionary<string, double>>
            {
                {DateTime.Today.AddDays(-3).ToString(), new Dictionary<string, double> { { "Patients", 0 }, { "Organizations", 0 }, {"Claims", 0 } } },
                {DateTime.Today.AddDays(-2).ToString(), new Dictionary<string, double> { { "Patients", 0 }, { "Organizations", 0 }, {"Claims", 0 } } },
                {DateTime.Today.AddDays(-1).ToString(), new Dictionary<string, double> { { "Patients", 0 }, { "Organizations", 0 }, {"Claims", 0 } } },
                {DateTime.Today.ToString(), new Dictionary<string, double> { { "Patients", 0 }, { "Organizations", 0 }, {"Claims", 0 } } }
            };
        }
    }
}