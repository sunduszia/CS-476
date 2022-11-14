using System;
namespace MyMentalHealth.Interface
{
    public interface IDailyCheckinsObserver
    {
        public void update(int Id, string Feeling, DateTime Date, int UserId);
    }
}


