using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyMentalHealth.Models
{
    public class DailyCheckins
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Feeling { get; set; }

        public DateTime Date { get; set; }


        public int UserId { get; set; }

        
    }
}

