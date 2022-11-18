﻿using System;
using MyMentalHealth.Models.Interface;

namespace MyMentalHealth.Models.Observers
{
    public class IssueItemService: IIssueItemsService
    {
        public List<IObserver> Observers = new List<IObserver>();


        public void Notify(IssueItems issueItems)
        {
            foreach(var observer in Observers)
            {
                observer.Update(issueItems);
            }
        }

        public void Register(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void UnRegister(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void UpdateIssueItem(IssueItems issueItems)
        {
            Notify(issueItems);
        }
    }
}

