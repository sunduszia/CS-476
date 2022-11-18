using System;
namespace MyMentalHealth.Models.Interface
{
    public interface IObserver
    {
        void Update(IssueItems issueItems);
    }
}
