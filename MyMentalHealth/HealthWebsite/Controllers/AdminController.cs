using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;

namespace MyMentalHealth.Controllers
{
    public class AdminController : Controller
    {
        private readonly MymentalhealthContext _context;

        public AdminController(MymentalhealthContext context)
        {
            _context = context;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(m => m.Email == loginModel.Email && m.Password == loginModel.Password && m.RoleId == 1);

                /*.FirstOrDefaultAsync(m => m.Email == loginModel.Email && m.Password == loginModel.Password); */
                if (user != null)
                {
                    return RedirectToAction(nameof(Index));
                    /*
                    Session["UserID"] = user.Id.ToString();
                    Session["FirstName"] = user.FirstName.ToString();
                    Session["LastName"] = user.LastName.ToString();
                    if (user.RoleId == 2)
                    {
                        Session["Role"] == "User";
                    }
                    */
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return RedirectToAction(nameof(Login));
                }

            }
            return View(loginModel);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}