using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyMentalHealth.Models
{
    
    public class AdminModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}

