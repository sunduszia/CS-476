using System;
namespace MyMentalHealth.Models.Interface
{
    public interface ISubject
    {
        void Register(IObserver observer);
        void UnRegister(IObserver observer);
        void Notify(IssueItems issueItems);

    }
}

