using System;
namespace MyMentalHealth.Interface
{
    public interface DailyCheckinsSubject
    {
        protected List<IDailyCheckinsObserver> observerList { get; }

        public void register(IDailyCheckinsObserver observer);
        public void unregister(IDailyCheckinsObserver observer);
        public void notify();
    }
}

