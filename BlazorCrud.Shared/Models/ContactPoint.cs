using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared.Models
{
    public class ContactPoint
    {
        public int Id { get; set; }
        public string System { get; set; }
        public string Value { get; set; }
        public string Use { get; set; }
    }
}