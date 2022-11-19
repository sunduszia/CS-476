using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyMentalHealth.Models
{
    public class RegistrationModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{9}$",
         ErrorMessage = "Student Id should contain exactly 9 digits")]
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
         ErrorMessage = "Password should contain at least one Upper case character, one lower case character, one digit, one special character, and must be 8 or more characters long")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}

