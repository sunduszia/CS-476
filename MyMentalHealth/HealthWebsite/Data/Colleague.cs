using System;
using MyMentalHealth.Models.Interface;

namespace MyMentalHealth.Models
{
    public abstract class Colleague
    {
        protected IMediator _mediator;

        public Colleague(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}

