using System;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;

namespace MyMentalHealth.Data
{
    public class DataFunctions : IDataFunctions
    {
        private readonly MymentalhealthContext _context;

        public DataFunctions(MymentalhealthContext context)
        {
            _context = context;

        }
        public async Task UpdateUserCategoryAsync(List<UserMentalHealthIssue> userIssuesToDelete, List<UserMentalHealthIssue> userIssuesToAdd)
        {
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.RemoveRange(userIssuesToDelete);
                    await _context.SaveChangesAsync();
                    if (userIssuesToAdd != null)
                    {
                        _context.AddRange(userIssuesToAdd);
                        await _context.SaveChangesAsync();
                    }
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {

                    await dbContextTransaction.DisposeAsync();

                }
            }
        }
    }
}

