using System;
using MyMentalHealth.Models;

namespace MyMentalHealth.Interface
{
    public interface ISubject
    {
        void Register(IObserver observer);
        void UnRegister(IObserver observer);
        void Notify(IssueItems issueItems);

    }
}

