using System;
namespace MyMentalHealth.Models
{
    public class MentalHealthIssueDetailsModel
    {
        public IEnumerable<GroupItemsByIssueModel> GroupItems { get; set; }
        public IEnumerable<MentalHealthIssues> Issues { get; set; }
    }
}

