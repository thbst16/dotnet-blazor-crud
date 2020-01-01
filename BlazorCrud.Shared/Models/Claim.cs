using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared.Models
{
    public class Claim
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Patient { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Organization { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
