using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared.Models
{
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
