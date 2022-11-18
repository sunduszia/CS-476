using System;
using MyMentalHealth.Models.Interface;

namespace MyMentalHealth.Models
{
    public abstract class Colleague
    {
        protected IMediator mediator;

        public Colleague(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}

