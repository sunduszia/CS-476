using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;

namespace MyMentalHealth.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserToIssueController : Controller
    {
        private readonly MymentalhealthContext _context;

        public UserToIssueController(MymentalhealthContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.MentalHealthIssues.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveUsers([Bind("MentalHealthIssueId,SelectedUsers")] UserIssueListModel user)
        {
            
            var userSelectedToAdd = await UsersToAdd(user);
            var userSelectedToDelete = await UsersToDelete(user.MentalHealthIssueId);
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.RemoveRange(userSelectedToDelete);
                    await _context.SaveChangesAsync();
                    if(userSelectedToAdd != null)
                    {
                        _context.AddRange(userSelectedToAdd);
                        await _context.SaveChangesAsync();
                    }
                }
                catch(Exception ex)
                {

                    await dbContextTransaction.DisposeAsync();

                }
            }

            user.Users = await GetAllUsers();

            return PartialView("_UserListPartial",user);
           

        }


        [HttpGet]
        public async Task<IActionResult> GetUsersForIssue(int mentalHealthIssueId)
        {
            UserIssueListModel user = new UserIssueListModel();
            var allUsers = await GetAllUsers();
            var selectedUsers = await GetSavedUsers(mentalHealthIssueId);

            user.Users = allUsers;
            user.SelectedUsers = selectedUsers;

            return PartialView("_UserListPartial", user);
        }


        private async Task<List<UserMentalHealthIssue>> UsersToAdd(UserIssueListModel users)
        {
            var usersToAdd = (from user in users.SelectedUsers
                              select new UserMentalHealthIssue
                              {
                                MentalHealthIssueId = users.MentalHealthIssueId,
                                UserId = user.Id
                              }).ToList();
            return await Task.FromResult(usersToAdd);
        }


        private async Task<List<UserMentalHealthIssue>> UsersToDelete(int mentalHealthIssueId)
        {
            var usersToDelete = await (from user in _context.UserMentalHealthIssues
                                       where user.MentalHealthIssueId == mentalHealthIssueId
                                       select new UserMentalHealthIssue
                                       {
                                           Id = user.Id,
                                           MentalHealthIssueId = mentalHealthIssueId,
                                           UserId = user.UserId
                                       }).ToListAsync();
            return usersToDelete;
        }


        private async Task<List<UserModel>> GetAllUsers()
        {
            var users = await (from user in _context.Users
                               where user.RoleId == 2
                               select new UserModel
                               {
                                   Id = user.Id,
                                   FirstName = user.FirstName,
                                   LastName = user.LastName
                               }).ToListAsync();
            return users;

        }


        private async Task<List<UserModel>> GetSavedUsers(int mentalHealthIssueId)
        {
            var savedUsers = await (from user in _context.UserMentalHealthIssues
                                    where user.MentalHealthIssueId == mentalHealthIssueId
                                    select new UserModel
                                    {
                                        Id = user.Id
                                    }).ToListAsync();
            return savedUsers;
        }
        


    }
}