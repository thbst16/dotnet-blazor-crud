using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Primary Care Provider")]
        public string PrimaryCareProvider { get; set; }
        public string State { get; set; }
        public List<ContactPoint> Contacts { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class ContactPoint
    {
        public int Id { get; set; }
        public string System { get; set; }
        public string Value { get; set; }
        public string Use { get; set; }
    }
}
