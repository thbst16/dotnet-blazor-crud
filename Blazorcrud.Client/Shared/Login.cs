using System.ComponentModel.DataAnnotations;

namespace Blazorcrud.Client.Shared
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}