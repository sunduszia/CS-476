using System;
using MyMentalHealth.Interface;
using MyMentalHealth.Models;

namespace MyMentalHealth.Observers
{
    public class DeleteObserver:IObserver
    {
        
        public void Update(IssueItems issueItems)
        {
            Console.WriteLine("IssueItem No '{0}' is deleted.",
             issueItems.Id);
        }

        
    }

}

