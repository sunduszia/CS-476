using System;
namespace MyMentalHealth.Models.Interface
{
    public interface IObserver<in T>
        where T : EventArgs
    {
        void Update(Object sender, T e);
    }
}

