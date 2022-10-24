using System;
using System.ComponentModel.DataAnnotations;

namespace MyMentalHealth.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
       
    }
}

