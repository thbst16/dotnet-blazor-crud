using System.Collections.Generic;

namespace BlazorCrud.Client.Shared
{
    public class ClaimModelView
    {
        public _Patient SelectedPatient { get; set; }
        public _Provider SelectedProvider { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public List<_LineItem> LineItems { get; set; }
    }

    public class _Patient
    {
        public _Patient() { }
        public _Patient(int id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class _Provider
    {
        public _Provider() { }
        public _Provider(int id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
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
