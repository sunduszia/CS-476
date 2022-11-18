using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MyMentalHealth.Models
{
    
    public class Article: Contents
    {
        //public int Id { get; set; }

        //[Required]
        //[StringLength(200, MinimumLength = 5)]
        //public string Title { get; set; }

        //[Display(Name = "HTML Content")]
        //public string HTMLContent { get; set; }



        //[NotMapped]
        //public int ItemIssueId { get; set; }
            //public int IssueItemsId { get; set; }
            //[NotMapped]
            //public int MentalHealthIssueId { get; set; }
    }
}

