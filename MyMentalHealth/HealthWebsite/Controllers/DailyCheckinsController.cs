using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;
using Newtonsoft.Json;
using MyMentalHealth.Interface;

namespace MyMentalHealth.Controllers
{
    [Authorize]
    public class DailyCheckinsController : Controller, Interface.IDailyCheckinsObserver
    {
        private readonly MymentalhealthContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IDailyCheckinsObserver _dailyCheckinsObserver;
        

        public DailyCheckinsController(MymentalhealthContext context, IHttpContextAccessor httpContextAccessor, IDailyCheckinsObserver dailyCheckinsObserver)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _dailyCheckinsObserver = dailyCheckinsObserver;
        }

        // GET: DailyCheckins
        public async Task<IActionResult> Index() 
        {
            int UserId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "Id").Value);

            //_dailyCheckinsObserver.update(_context.DailyCheckins.Id, Feeling, Date, UserId) { }

            var events = _context.DailyCheckins.Where(x => x.UserId == UserId).Select(x => new
            {
                title = x.Feeling,
                start = x.Date.ToString("yyyy-MM-dd")
            }).ToList();

            string eventsString = JsonConvert.SerializeObject(events);

            ViewBag.Events = eventsString;

            return View(await _context.DailyCheckins.Where(x => x.UserId == UserId).ToListAsync());
        }

        // GET: DailyCheckins/Create
        public IActionResult Create()
        {
            int UserId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "Id").Value);

            ViewData["UserId"] = UserId;

            int checkInCompleted = _context.DailyCheckins
                .Where(x => x.UserId == UserId)
                .Where(x => x.Date >= DateTime.Now.Date.AddDays(-1))
                .Count();

            if (checkInCompleted == 0)
            {
                ViewBag.DailyCheckinComplete = "false";
            }
            else
            {
                ViewBag.DailyCheckinComplete = "true";
            }


            return View();
        }

        // POST: DailyCheckins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Feeling,Date,UserId")] DailyCheckins dailyCheckins)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyCheckins);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyCheckins);
        }

        // GET: DailyCheckins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DailyCheckins == null)
            {
                return NotFound();
            }

            var dailyCheckins = await _context.DailyCheckins.FindAsync(id);
            if (dailyCheckins == null)
            {
                return NotFound();
            }
            return View(dailyCheckins);
        }

        // POST: DailyCheckins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Feeling,Date,UserId")] DailyCheckins dailyCheckins)
        {
            if (id != dailyCheckins.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyCheckins);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyCheckinsExists(dailyCheckins.Id))
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
            return View(dailyCheckins);
        }

        // GET: DailyCheckins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DailyCheckins == null)
            {
                return NotFound();
            }

            var dailyCheckins = await _context.DailyCheckins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyCheckins == null)
            {
                return NotFound();
            }

            return View(dailyCheckins);
        }

        // POST: DailyCheckins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DailyCheckins == null)
            {
                return Problem("Entity set 'MymentalhealthContext.DailyCheckins'  is null.");
            }
            var dailyCheckins = await _context.DailyCheckins.FindAsync(id);
            if (dailyCheckins != null)
            {
                _context.DailyCheckins.Remove(dailyCheckins);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyCheckinsExists(int id)
        {
            return _context.DailyCheckins.Any(e => e.Id == id);
        }

        public void update(int Id, string Feeling, DateTime Date, int UserId)
        {
            throw new NotImplementedException();
        }
    }
}