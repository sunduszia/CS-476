using System;
namespace MyMentalHealth.Models
{
    public class GroupItemsByIssueModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IGrouping<int, IssueItemsDetailsModel> Items { get; set; }
    }
}

