using System.ComponentModel.DataAnnotations;

namespace Whomever.Models
{
    public class ContactViewModel
    {
        [Required]
        //[MinLength(3)]
        [MaxLength(100, ErrorMessage = "Name can't be more than 100 characters")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Subject can't be more than 50 characters")]
        public string Subject { get; set; }

        [Required]
        [MaxLength(350, ErrorMessage = "Too long")]
        public string Message { get; set; }
    }
}