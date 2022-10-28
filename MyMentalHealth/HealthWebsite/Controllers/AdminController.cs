using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return _context.AdminModels != null ?
                        View(await _context.AdminModels.ToListAsync()) :
                        Problem("Entity set 'AdminContext.AdminModel'  is null.");
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdminModels == null)
            {
                return NotFound();
            }

            var adminModel = await _context.AdminModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminModel == null)
            {
                return NotFound();
            }

            return View(adminModel);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password")] AdminModel adminModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminModel);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdminModels == null)
            {
                return NotFound();
            }

            var adminModel = await _context.AdminModels.FindAsync(id);
            if (adminModel == null)
            {
                return NotFound();
            }
            return View(adminModel);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password")] AdminModel adminModel)
        {
            if (id != adminModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminModelExists(adminModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adminModel);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdminModels == null)
            {
                return NotFound();
            }

            var adminModel = await _context.AdminModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminModel == null)
            {
                return NotFound();
            }

            return View(adminModel);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdminModels == null)
            {
                return Problem("Entity set 'AdminContext.AdminModel'  is null.");
            }
            var adminModel = await _context.AdminModels.FindAsync(id);
            if (adminModel != null)
            {
                _context.AdminModels.Remove(adminModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminModelExists(int id)
        {
            return (_context.AdminModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public ActionResult Login()
        {
            return View();
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(UserProfile objUser)
        {
            if (ModelState.IsValid)
            {
                using (DB_Entities db = new DB_Entities())
                {
                    var obj = db.UserProfiles.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }
        public ActionResult AdminDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        */
    }
}