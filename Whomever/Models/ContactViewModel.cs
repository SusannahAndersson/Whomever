using System.ComponentModel.DataAnnotations;

namespace Whomever.Models
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Too long")]
        public string Message { get; set; }
    }
}