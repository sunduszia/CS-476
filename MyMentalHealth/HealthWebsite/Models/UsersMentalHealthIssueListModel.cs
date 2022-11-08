using System;
namespace MyMentalHealth.Models
{
    public class UsersMentalHealthIssueListModel
    {
        
        public int MentalHealthIssueId { get; set; }

        public ICollection<UserModel> Users { get; set; }

        public ICollection<UserModel> SelectedUsers { get; set; }

    }
}

