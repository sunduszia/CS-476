using System;
namespace MyMentalHealth.Models
{
    public class IssuesToUserModel
    {
        public int UserId { get; set; }
        public ICollection<MentalHealthIssues> MentalHealthIssues { get; set; }
        public ICollection<MentalHealthIssues> IssuesSelected { get; set; }

    }
}

