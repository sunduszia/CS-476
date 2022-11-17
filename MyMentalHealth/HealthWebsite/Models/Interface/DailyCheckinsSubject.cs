using System;
namespace MyMentalHealth.Interface
{
    public interface DailyCheckinsSubject
    {
        public List<IDailyCheckinsObserver> observerList { get; set; }

        public void register(IDailyCheckinsObserver observer);
        public void unregister(IDailyCheckinsObserver observer);
        public void notify();
    }
}

