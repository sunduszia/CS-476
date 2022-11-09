using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;

namespace MyMentalHealth.Controllers
{
    public class UserManageController : Controller
    {
        private readonly MymentalhealthContext _context;

        public UserManageController(MymentalhealthContext context)
        {
            _context = context;
        }

        
        
        [Authorize(Roles = "Admin")]
        // GET: User
        public async Task<IActionResult> Index()
        {
            List<Users> users = await (from user in _context.Users
                                       where user.RoleId == 2
                                       select new Users
                                       {
                                           Id = user.Id,
                                           FirstName = user.FirstName,
                                           LastName = user.LastName,
                                           Email = user.Email,
                                           Password = user.Password,
                                           StudentId = user.StudentId,
                                           RoleId = user.RoleId
                                       }).ToListAsync();
              return View(users);
        }
        [Authorize(Roles = "Admin")]
        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }
        [Authorize(Roles = "Admin")]
        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password,StudentId,RoleId")] UsersMapping users)
        {
            if (ModelState.IsValid)
            {
                Users newUser = new Users
                {
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    Email = users.Email,
                    Password = users.Password,
                    StudentId = users.StudentId,
                    RoleId = users.RoleId
                };

                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }
        [Authorize(Roles = "Admin")]
        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            UsersMapping newUser = new UsersMapping
            {
                Id = users.Id,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                Password = users.Password,
                StudentId = users.StudentId,
                RoleId = users.RoleId
            };
            return View(newUser);
        }
        [Authorize(Roles = "Admin")]
        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Password,StudentId,RoleId")] UsersMapping users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Users newUser = new Users
                    {
                        Id = users.Id,
                        FirstName = users.FirstName,
                        LastName = users.LastName,
                        Email = users.Email,
                        Password = users.Password,
                        StudentId = users.StudentId,
                        RoleId = users.RoleId,

                    };
                    _context.Update(newUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
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
            return View(users);
        }
        [Authorize(Roles = "Admin")]
        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }
        [Authorize(Roles = "Admin")]
        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'MymentalhealthContext.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
          return _context.Users.Any(e => e.Id == id);
        }
    }
}
