using System;
using MyMentalHealth.Models;

namespace MyMentalHealth.Data
{
    public interface IDataFunctions
    {
        Task UpdateUserCategoryAsync(List<UserMentalHealthIssue> userIssuesToDelete, List<UserMentalHealthIssue> userIssuesToAdd);
    }
}

