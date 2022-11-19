using System;
using MyMentalHealth.Models;

namespace MyMentalHealth.Interface
{
    public interface IObserver
    {
        void Update(IssueItems issueItems);
    }
}
