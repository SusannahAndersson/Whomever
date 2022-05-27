using Microsoft.AspNetCore.Identity;

namespace Whomever.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }

        //public int PhoneNumber { get; set; }
        //public string Address { get; set; }
        //public bool IsEmailConfirmed { get; set; }
    }
}