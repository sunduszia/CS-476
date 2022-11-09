using System;
using System.ComponentModel.DataAnnotations;

namespace MyMentalHealth.Models
{
    public class MentalHealthIssuesMapping
    {

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

