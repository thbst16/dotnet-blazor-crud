using System.Collections.Generic;

namespace BlazorCrud.Shared.Models
{
    public class Dashboard
    {
        public Dictionary<string, double> PatientsByState { get; set; }
        public Dictionary<string, double> OrganizationsByType { get; set; }
        public Dictionary<string, double> ClaimsByType { get; set; }
        public Dictionary<string, Dictionary<string, int>> UpdatedEntitiesByDate { get; set; }

        public Dashboard()
        {
            PatientsByState = new Dictionary<string, double>();
            OrganizationsByType = new Dictionary<string, double>();
            ClaimsByType = new Dictionary<string, double>();
            UpdatedEntitiesByDate = new Dictionary<string, Dictionary<string, int>>();
        }
    }
}