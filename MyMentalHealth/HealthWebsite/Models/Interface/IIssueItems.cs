using System;
namespace MyMentalHealth.Models.Interface
{
    public interface IIssueItemsService: ISubject
    {
        void UpdateIssueItem(IssueItems issueItems);
    }
}

