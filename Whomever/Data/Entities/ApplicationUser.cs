using Microsoft.AspNetCore.Identity;

namespace Whomever.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }

        //public int PhoneNumber { get; set; }
        //public string Address { get; set; }
        //public bool IsEmailConfirmed { get; set; }
    }
}