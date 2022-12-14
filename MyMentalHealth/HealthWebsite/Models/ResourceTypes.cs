using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMentalHealth.Models
{
    public class ResourceTypes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        [ForeignKey("ResourceTypeId")]
        public virtual ICollection<IssueItems> IssueItems { get; set; }

    }
}

