using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyMentalHealth.Models.Interface;

namespace MyMentalHealth.Models
{
    //public class HTMLContentChangedEventArgs: EventArgs
    //{
    //    public string HTMLContent { get; set; }
    //}
    //public class Contents: ISubject<HTMLContentChangedEventArgs>
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    [Required]
    //    [StringLength(200, MinimumLength = 5)]
    //    public string Title { get; set; }

    //    //[Display(Name = "HTML Content")]
    //    //public string HTMLContent { get; set; }

    //    [Display(Name = "Video Link")]
    //    public string VideoLink { get; set; }

    //    public IssueItems IssueItems { get; set; }

    //    //[NotMapped]
    //    //public int ItemIssueId { get; set; }
    //    public int IssueItemsId { get; set; }
    //    [NotMapped]
    //    public int MentalHealthIssueId { get; set; }

    //    [NotMapped]
    //    public string _htmlContent { get; set; }

    //    [Display(Name = "HTML Content")]
    //    public string HTMLContent
    //    {
    //        get { return _htmlContent; }
    //        set
    //        {
    //            _htmlContent = value;
    //            OnHtmlContentChanged(value);
    //        }
    //    }

    //    private void OnHtmlContentChanged(string value)
    //    {
    //        if(HTMLContentChanged != null)
    //        {
    //            HTMLContentChanged(this, new HTMLContentChangedEventArgs() { HTMLContent = value});
    //        }
    //    }

    //    public EventHandler<HTMLContentChangedEventArgs> HTMLContentChanged;
    //    public void Register(Interface.IObserver<HTMLContentChangedEventArgs> observer)
    //    {
    //        HTMLContentChanged += observer.Update;
    //    }

    //    public void UnRegister(Interface.IObserver<HTMLContentChangedEventArgs> observer)
    //    {
    //        HTMLContentChanged += observer.Update;
    //    }
    //}
    public abstract class Contents 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        [Display(Name = "HTML Content")]
        public string HTMLContent { get; set; }

        //[Display(Name = "Video Link")]
        //public string VideoLink { get; set; }

        public IssueItems IssueItems { get; set; }

        //[NotMapped]
        //public int ItemIssueId { get; set; }
        public int IssueItemsId { get; set; }
        [NotMapped]
        public int MentalHealthIssueId { get; set; }

        public string DiscriminatorValue
        {
            get
            {
                return this.GetType().Name;
            }
        }
    }
}

