using System;
using MyMentalHealth.Models.Interface;

namespace MyMentalHealth.Models.Observers
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

