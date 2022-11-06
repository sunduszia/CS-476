using System;
using System.ComponentModel.DataAnnotations;

namespace MyMentalHealth.Models
{
    public class UserMentalHealthIssue
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MentalHealthIssueId { get; set; }

    }
}

