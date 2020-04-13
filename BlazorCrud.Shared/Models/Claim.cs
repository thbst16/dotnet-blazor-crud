using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string Patient { get; set; }
        public string Organization { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public List<LineItem> LineItems { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class LineItem
    {
        public int Id { get; set; }
        public string Service { get; set; }
        public decimal Amount { get; set; }
    }
}
