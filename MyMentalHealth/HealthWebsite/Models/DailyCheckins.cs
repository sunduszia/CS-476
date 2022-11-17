using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using MyMentalHealth.Interface;

namespace MyMentalHealth.Models
{
    public class DailyCheckins 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Feeling { get; set; }

        [DisplayFormat(DataFormatString = "{0:D}")]
        public DateTime Date { get; set; }

        public int UserId { get; set; }

        //List<IDailyCheckinsObserver> DailyCheckinsSubject.observerList => throw new NotImplementedException();

        //void DailyCheckinsSubject.notify()
        //{
        //    foreach (IDailyCheckinsObserver observer in DailyCheckinsSubject.observerList)
        //    {
        //        observer.update(Id, Feeling, Date, UserId);
        //    }
        //}

        //void DailyCheckinsSubject.register(IDailyCheckinsObserver observer)
        //{
        //    observerList.Add(observer);
        //}

        //void DailyCheckinsSubject.unregister(IDailyCheckinsObserver observer)
        //{
        //    observerList.Remove(observer);
        //}

        //void getState()
        //{
        //    return subjectstate
        //}
    }
}

