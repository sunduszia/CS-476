using System;
namespace MyMentalHealth.Models.Interface
{
    public interface ISubject<out T>
        where T : EventArgs
    {
        void Register(IObserver<T> observer);
        void UnRegister(IObserver<T> observer);
    }
}

