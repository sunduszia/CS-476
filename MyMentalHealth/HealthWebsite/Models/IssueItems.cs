using System;
using System.ComponentModel.DataAnnotations;

namespace MyMentalHealth.Models
{
    public class IssueItems
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int MentalHealthIssueId { get; set; }

        public int ResourceTypeId { get; set; }
    }
}

