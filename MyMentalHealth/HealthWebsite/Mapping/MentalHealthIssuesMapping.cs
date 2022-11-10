using System;
using System.ComponentModel.DataAnnotations;

namespace MyMentalHealth.Models
{
    public class MentalHealthIssuesMapping
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

