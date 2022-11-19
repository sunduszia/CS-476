using System;
using MyMentalHealth.Models;

namespace MyMentalHealth.Interface
{
    public interface IIssueItemsService: ISubject
    {
        void UpdateIssueItem(IssueItems issueItems);
    }
}

