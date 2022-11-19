using System;
using MyMentalHealth.Interface;
using MyMentalHealth.Models;

namespace MyMentalHealth.Observers
{
    public class EditObserver: IObserver
    {
       
        public void Update(IssueItems issueItems)
        {
            Console.WriteLine("IssueItem No '{0}' is updated.",
             issueItems.Id);
        }

        
    }
}

