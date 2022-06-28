using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Whomever.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //[MaxLength(40, ErrorMessage = "Username can't be more than 40 characters"),
        //MinLength(5, ErrorMessage = "Username can't be less than 5 characters")]
        //public string UserName { get; set; }
        //public string Password { get; set; }
        //[EmailAddress]]
        //public string? Email { get; set; }
        [MaxLength(40, ErrorMessage = "First Name can't be more than 40 characters"),
MinLength(2, ErrorMessage = "First Name can't be less than 2 characters")]
        public string? FirstName { get; set; }

        [MaxLength(60, ErrorMessage = "Surname can't be more than 60 characters"),
MinLength(5, ErrorMessage = "Surname can't be less than 5 characters")]
        public string? SurName { get; set; }

        //[Phone]
        //[MaxLength(40)]
        //public int PhoneNumber { get; set; }
        //[MaxLength(200)]
        //public string Address { get; set; }
        //public bool IsEmailConfirmed { get; set; }
    }
}