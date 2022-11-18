using System;
using MyMentalHealth.Models;

namespace MyMentalHealth.Models
{
    public abstract class Mediator
    {
        public abstract int Send(Colleague colleague);
    }
}

