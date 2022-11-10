using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyMentalHealth.Models
{
    public class DailyCheckins
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Feeling { get; set; }

        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime Date { get; set; }

        public int UserId { get; set; }

    }
}

