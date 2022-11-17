using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyMentalHealth.Models.Interface;

namespace MyMentalHealth.Models
{
    public class HTMLContentChangedEventArgs : EventArgs
    {
        public string HTMLContent { get; set; }
    }

    public class Contents: ISubject<HTMLContentChangedEventArgs>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }


        //public string HTMLContent { get; set; }
        [NotMapped]
        private string _htmlContent;

        [Display(Name = "HTML Content")]
        public string HTMLContent
        {
            get { return _htmlContent; }
            set
            {
                _htmlContent = value;
                OnHTMLContentChanged(value);
            }
        }
        private void OnHTMLContentChanged(string value)
        {
            if (HTMLContentChanged != null)
            {
                HTMLContentChanged(this, new HTMLContentChangedEventArgs() { HTMLContent = value });
            }
        }
        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }

        public IssueItems IssueItems { get; set; }

        [NotMapped]
        public int ItemIssueId { get; set; }

        [NotMapped]
        public int MentalHealthIssueId { get; set; }

        public event EventHandler<HTMLContentChangedEventArgs> HTMLContentChanged;

        public void Register(Interface.IObserver<HTMLContentChangedEventArgs> observer)
        {
            HTMLContentChanged += observer.Update;
        }

        public void UnRegister(Interface.IObserver<HTMLContentChangedEventArgs> observer)
        {
            HTMLContentChanged += observer.Update;
        }
    }
}

