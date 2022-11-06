using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMentalHealth.Models
{
    public class Contents
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        [Display(Name = "HTML Content")]
        public string HTMLContent { get; set; }

        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }

        public IssueItems IssueItems { get; set; }

        [NotMapped]
        public int ItemIssueId { get; set; }

        [NotMapped]
        public int MentalHealthIssueId { get; set; }
    }
}

