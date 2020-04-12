using BlazorCrud.Shared.Models;
using System.Collections.Generic;

namespace BlazorCrud.Client.Shared
{
    public class ClaimModelView
    {
        public Patient SelectedPatient { get; set; }
        public Organization SelectedOrganization { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public List<_LineItem> LineItems { get; set; }
    }

    public class _LineItem
    {
        public _LineItem() { }
        public _LineItem(int id, string service, decimal amount)
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
