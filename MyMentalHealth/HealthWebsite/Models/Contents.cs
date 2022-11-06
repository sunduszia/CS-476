using System;
using System.ComponentModel.DataAnnotations;

namespace MyMentalHealth.Models
{
    public class Contents
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        public IssueItems IssueItems { get; set; }
    }
}

