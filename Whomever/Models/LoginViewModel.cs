using System.ComponentModel.DataAnnotations;

namespace Whomever.Models
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        //[Required]
        //public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}