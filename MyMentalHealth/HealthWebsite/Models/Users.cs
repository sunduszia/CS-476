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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<UserMentalHealthIssue> UserMentalHealthIssues { get; set; }

    }
}

