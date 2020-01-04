using System.Collections.Generic;

namespace BlazorCrud.Shared.Models
{
    public class Dashboard
    {
        public Dictionary<string, int> PatientsByState { get; set; }
        public Dictionary<string, int> OrganizationsByType { get; set; }
        public Dictionary<string, int> ClaimsByType { get; set; }
        public Dictionary<string, Dictionary<string, int>> UpdatedEntitiesByDate { get; set; }

        public Dashboard()
        {
            PatientsByState = new Dictionary<string, int>();
            OrganizationsByType = new Dictionary<string, int>();
            ClaimsByType = new Dictionary<string, int>();
            UpdatedEntitiesByDate = new Dictionary<string, Dictionary<string, int>>();
        }
    }
}