using System.ComponentModel.DataAnnotations;

namespace Whomever.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}