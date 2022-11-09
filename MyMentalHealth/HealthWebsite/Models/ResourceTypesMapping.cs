using System;
using System.ComponentModel.DataAnnotations;

namespace MyMentalHealth.Models
{
    public class ResourceTypesMapping
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
    }
}

