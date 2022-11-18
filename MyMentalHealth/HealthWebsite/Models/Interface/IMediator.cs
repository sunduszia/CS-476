using System;
using MyMentalHealth.Data;

namespace MyMentalHealth.Models.Interface
{
    public interface IMediator
    {
        void SendMessage(Colleague caller, string msg);

    }
}

