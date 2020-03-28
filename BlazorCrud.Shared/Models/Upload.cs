using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared.Models
{
    public class Upload
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string FileType { get; set; }
        [Required]
        public DateTime UploadTimestamp { get; set; }
        public DateTime? ProcessedTimestamp { get; set; }
        [Required]
        public string FileContent { get; set; }
    }
}
