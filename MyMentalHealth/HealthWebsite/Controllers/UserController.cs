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
using MyMentalHealth.Models;

namespace MyMentalHealth.Controllers
{
    public class UserController : Controller
    {
        private readonly MymentalhealthContext _context;

        public UserController(MymentalhealthContext context)
        {
            _context = context;
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
                        new Claim("LastName", user.LastName),
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

        [AllowAnonymous]
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


    }
}