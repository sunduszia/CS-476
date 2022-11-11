using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyMentalHealth.Models
{
    public class IssueItemsMapping
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        public string Description { get; set; }


        public int MentalHealthIssueId { get; set; }

        [Required(ErrorMessage = "Please choose a valid item from the dropdown list")]
        [Display(Name = "Resource Type")]
        public int ResourceTypeId { get; set; }

        //public virtual ICollection<SelectListItem> ResourceTypes { get; set; }

        //public List<ResourceTypes> ResourceTypes { get; set; }





    }
}

