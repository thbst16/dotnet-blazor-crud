using System;
using System.Collections.Generic;

namespace BlazorCrud.Shared.Models
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public List<Address> Addresses { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
