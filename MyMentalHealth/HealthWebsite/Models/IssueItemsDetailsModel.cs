using System;
namespace MyMentalHealth.Models
{
    public class IssueItemsDetailsModel
    {
        public int MentalHealthIssueId { get; set; }
        public string MentalHealthIssueTitle { get; set; }
        public int IssueItemId { get; set; }
        public string IssueItemTitle { get; set; }
        public string IssueItemDescription { get; set; }
        public int ResourceTypeId { get; set; }
    }
}

