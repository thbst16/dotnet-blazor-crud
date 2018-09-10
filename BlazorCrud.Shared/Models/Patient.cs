using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PrimaryCareProvider { get; set; }
        [Required]
        public string State{ get; set; }
    }
}