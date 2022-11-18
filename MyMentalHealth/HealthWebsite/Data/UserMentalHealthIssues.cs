using System;
using System.ComponentModel.DataAnnotations;
using MyMentalHealth.Models.Interface;

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

