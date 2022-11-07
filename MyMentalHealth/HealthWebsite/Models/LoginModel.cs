using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyMentalHealth.Models
{
    public class LoginModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }





    }
}

