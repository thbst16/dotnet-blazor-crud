using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared.Models
{
    public class ContactPoint
    {
        public int Id { get; set; }
        //[Required]
        public string Type { get; set; }
        //[Required]
        public string Number { get; set; }
        //[Required]
        public string Use { get; set; }
    }
}
