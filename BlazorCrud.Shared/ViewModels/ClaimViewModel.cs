using BlazorCrud.Shared.Models;
using System.Collections.Generic;

namespace BlazorCrud.Shared.ViewModels
{
    public class ClaimViewModel
    {
        public int Id { get; set; }
        public Patient SelectedPatient { get; set; }
        public Organization SelectedOrganization { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public List<LineItem> LineItems { get; set; }
    }

    public class LineItem
    {
        public LineItem() { }
        public LineItem(int id, string service, decimal amount)
        {
            Id = id;
            Service = service;
            Amount = amount;
        }
        public int Id { get; set; }
        public string Service { get; set; }
        public decimal Amount { get; set; }
    }
}
