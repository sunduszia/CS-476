using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MyMentalHealth.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<UserMentalHealthIssue> UserMentalHealthIssues { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<DailyCheckins> DailyCheckins { get; set; }

    }
}

