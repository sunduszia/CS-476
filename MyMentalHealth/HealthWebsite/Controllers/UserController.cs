using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Data;
using MyMentalHealth.Models;

namespace MyMentalHealth.Controllers
{
    public class UserController : Controller
    {
        private readonly MymentalhealthContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataFunctions _dataFunctions;



        public UserController(MymentalhealthContext context, IHttpContextAccessor httpContextAccessor, IDataFunctions dataFunctions)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _dataFunctions = dataFunctions;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == loginModel.Email && m.Password == loginModel.Password && m.RoleId == 2);

                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("FirstName", user.FirstName),
                        new Claim("Email", user.Email),
                        new Claim("Password", user.Password),
                        new Claim("LastName", user.LastName),
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, "User"),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(claimsIdentity);
                    var authProperties = new AuthenticationProperties() { IsPersistent = loginModel.RememberMe };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal, authProperties);

                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ViewData["LoginError"] = "Invalid Email and/or Password. Please try again.";
                    return View(loginModel);
                }

            }
            return View(loginModel);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index),"Home");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Register(RegistrationModel registrationModel)
        {
            if (ModelState.IsValid)
            {
                var resultId = StudentIdExists(registrationModel.StudentId);
                if(resultId == true)
                {
                    ViewData["RegistrationEmailError"] = "There is an another account using the given Student Id";
                    return View(registrationModel);
                }
                else
                {
                    var resultEmail =  EmailExists(registrationModel.Email);
                    if (resultEmail == true)
                    {
                        ViewData["RegistrationIdError"] = "There is an another account using the given email";
                        return View(registrationModel);
                    }
                    else
                    {
                        Users newUser = new Users
                        {
                            FirstName = registrationModel.FirstName,
                            LastName = registrationModel.LastName,
                            Email = registrationModel.Email,
                            Password = registrationModel.Password,
                            StudentId = registrationModel.StudentId,
                            RoleId = 2
                        };
                        _context.Add(newUser);
                         _context.SaveChanges();
                        var tempUser = _context.Users.Any(u => u.Email.ToUpper() == newUser.Email.ToUpper());
                        if(tempUser)
                        {
                            return RedirectToAction(nameof(Login));
                        }
                        return View(registrationModel);
                    }
                    
                }
               
            }
            return View(registrationModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> IssueContent()
        {
            int UserId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "Id").Value);
            IEnumerable<IssueItemsDetailsModel> issues = null;
            IEnumerable<GroupItemsByIssueModel> items = null;
            MentalHealthIssueDetailsModel mentalHealthIssue = new MentalHealthIssueDetailsModel();

            issues = await GetIssuesItemDetails(UserId);
            items = GetGroupedItemsByIssues(issues);

            mentalHealthIssue.GroupItems = items;


            return View(mentalHealthIssue);
        }

        public async Task<IActionResult> ChooseIssues()
        {
            IssuesToUserModel issuesToUser = new IssuesToUserModel();
            int userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "Id").Value);

            issuesToUser.MentalHealthIssues = await GetItemsWithContent();
            issuesToUser.IssuesSelected = await GetItemsSavedForUser(userId);
            issuesToUser.UserId = userId;
            return View(issuesToUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseIssues(int[] issuesSelected)
        {
            int userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "Id").Value);
            /*foreach (var issue in issuesSelected)
            {
                var result = _context.
            }

            */
            
            List<UserMentalHealthIssue> userIssuesToDelete =  GetIssuesToDelete(userId);
            List<UserMentalHealthIssue> userIssuesToAdd = GetIssuesToAdd(issuesSelected, userId);
            using (var dbContextTransaction =  _context.Database.BeginTransaction())
            {
                try
                {
                    _context.RemoveRange(userIssuesToDelete);
                    _context.SaveChanges();
                    if (userIssuesToAdd != null)
                    {
                        _context.AddRange(userIssuesToAdd);
                        _context.SaveChanges();
                    }
                    dbContextTransaction.Commit();

                }
                catch (Exception ex)
                {

                     dbContextTransaction.Dispose();

                }
            }
            //await _dataFunctions.UpdateUserCategoryAsync(userIssuesToDelete,userIssuesToAdd);*/
            return RedirectToAction("IssueContent");
        }

        public bool EmailExists(string email)
        {
            bool emailExists =  _context.Users.Any(u => u.Email.ToUpper() == email.ToUpper());

            if (emailExists)
            {
                return true;

            }
            return false;

        }

        public bool StudentIdExists(int studentId)
        {
            bool studentIdExists =  _context.Users.Any(u => u.StudentId == studentId);

            if (studentIdExists)
            {
                return true;

            }
            return false;

        }

        private async Task<List<MentalHealthIssues>> GetItemsWithContent()
        {
            var itemsWithContent = await (from issues in _context.MentalHealthIssues
                                               join issueItem in _context.IssueItems
                                               on issues.Id equals issueItem.MentalHealthIssueId
                                               join content in _context.Contents
                                               on issueItem.Id equals content.IssueItems.Id
                                               select new MentalHealthIssues
                                               {
                                                   Id = issues.Id,
                                                   Title = issues.Title,
                                                   Description = issueItem.Description,
                                                   
                                               }).Distinct().ToListAsync();
            return itemsWithContent;

        }
        private async Task<List<MentalHealthIssues>> GetItemsSavedForUser(int userId)
        {
            var itemsSavedForUser = await (from user in _context.UserMentalHealthIssues
                                          where user.UserId == userId
                                          select new MentalHealthIssues
                                          {
                                              Id = user.MentalHealthIssueId
                                          }).ToListAsync();
            return itemsSavedForUser;

        }

        private List<UserMentalHealthIssue> GetIssuesToDelete(int userId)
        {
            var issuesToDelete =  (from user in _context.UserMentalHealthIssues
                                           where user.UserId == userId
                                           select new UserMentalHealthIssue
                                           {
                                               Id = user.Id,
                                               MentalHealthIssueId = user.MentalHealthIssueId,
                                               UserId = user.UserId
                                           }).ToList();
            return issuesToDelete;

        }

        private List<UserMentalHealthIssue> GetIssuesToAdd(int[] issuesSelected, int userId)
        {
            var issuesToAdd = (from mentalHealthIssueId in issuesSelected 
                               select new UserMentalHealthIssue
                               {
                                   UserId = userId,
                                   //MentalHealthIssueId = int.Parse(mentalHealthIssueId)
                                   MentalHealthIssueId = mentalHealthIssueId
                               }).ToList();
            return issuesToAdd;

        }

        private IEnumerable<GroupItemsByIssueModel> GetGroupedItemsByIssues(IEnumerable<IssueItemsDetailsModel> issueItems)
        {
            return from item in issueItems
                   group item by item.MentalHealthIssueId into grp
                   select new GroupItemsByIssueModel
                   {
                       Id = grp.Key,
                       Title = grp.Select(c => c.MentalHealthIssueTitle).FirstOrDefault(),
                       Items = grp
                   };
        }

        private async Task<IEnumerable<IssueItemsDetailsModel>> GetIssuesItemDetails(int userId)
        {
            return await (from item in _context.IssueItems
                          join mentalHealthIssue in _context.MentalHealthIssues
                          on item.MentalHealthIssueId equals mentalHealthIssue.Id
                          join content in _context.Contents
                          on item.Id equals content.IssueItems.Id
                          join user in _context.UserMentalHealthIssues
                          on mentalHealthIssue.Id equals user.MentalHealthIssueId
                          join resource in _context.ResourceTypes
                          on item.ResourceTypeId equals resource.Id
                          where user.UserId == userId
                          select new IssueItemsDetailsModel
                          {
                              MentalHealthIssueId    = mentalHealthIssue.Id,
                              MentalHealthIssueTitle = mentalHealthIssue.Title,
                              IssueItemId = item.Id,
                              IssueItemTitle = item.Title,
                              IssueItemDescription = item.Description
                              
                          }).ToListAsync();
        }


    }
}