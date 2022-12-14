using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MyMentalHealth.Models
{
    public class UserRoles
    {
        [Key]
        public int Id { get; set; }

        public int Role { get; set; }

        [ForeignKey("RoleId")]
        public ICollection<Users> Users { get; set; }


    }
}

