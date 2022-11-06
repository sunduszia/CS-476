using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMentalHealth.Models
{
    public class IssueItems
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int MentalHealthIssueId { get; set; }

        [Required(ErrorMessage = "Please choose a valid item from the dropdown list")]
        [Display(Name = "Resource Type")]
        public int ResourceTypeId { get; set; }

        [NotMapped]
        public virtual ICollection<SelectListItem> ResourceTypes { get; set; }

        [NotMapped]
        public int ContentId { get; set; }
    }
}

