using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyMentalHealth.Models.Interface;


namespace MyMentalHealth.Models
{
    public class MentalHealthIssues
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        public string Description { get; set; }

        
        [ForeignKey("MentalHealthIssueId")]
        public virtual ICollection<IssueItems> IssueItems { get; set; }

        [ForeignKey("MentalHealthIssueId")]
        public virtual ICollection<UserMentalHealthIssue> UserMentalHealthIssues { get; set; }

        //public MentalHealthIssues(IMediator mediator) : base(mediator) { }

        //public int Send(MentalHealthIssues mentalHealthIssues)
        //{
        //    return mediator.Send(this);
        //}
        
    }
}

