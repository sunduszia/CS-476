
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;

namespace MyMentalHealth.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MentalHealthIssueController : Controller
    {
        private readonly MymentalhealthContext _context;

        public MentalHealthIssueController(MymentalhealthContext context)
        {
            _context = context;
        }

        // GET: MentalHealthIssue
        public async Task<IActionResult> Index()
        {
              return View(await _context.MentalHealthIssues.ToListAsync());
        }

        // GET: MentalHealthIssue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MentalHealthIssues == null)
            {
                return NotFound();
            }

            var mentalHealthIssues = await _context.MentalHealthIssues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mentalHealthIssues == null)
            {
                return NotFound();
            }

            return View(mentalHealthIssues);
        }

        // GET: MentalHealthIssue/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MentalHealthIssue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] MentalHealthIssues mentalHealthIssues)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mentalHealthIssues);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mentalHealthIssues);
        }

        // GET: MentalHealthIssue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MentalHealthIssues == null)
            {
                return NotFound();
            }

            var mentalHealthIssues = await _context.MentalHealthIssues.FindAsync(id);
            if (mentalHealthIssues == null)
            {
                return NotFound();
            }
            return View(mentalHealthIssues);
        }

        // POST: MentalHealthIssue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] MentalHealthIssues mentalHealthIssues)
        {
            if (id != mentalHealthIssues.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mentalHealthIssues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MentalHealthIssuesExists(mentalHealthIssues.Id))
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
            return View(mentalHealthIssues);
        }

        // GET: MentalHealthIssue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MentalHealthIssues == null)
            {
                return NotFound();
            }

            var mentalHealthIssues = await _context.MentalHealthIssues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mentalHealthIssues == null)
            {
                return NotFound();
            }

            return View(mentalHealthIssues);
        }

        // POST: MentalHealthIssue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MentalHealthIssues == null)
            {
                return Problem("Entity set 'MymentalhealthContext.MentalHealthIssues'  is null.");
            }
            var mentalHealthIssues = await _context.MentalHealthIssues.FindAsync(id);
            if (mentalHealthIssues != null)
            {
                _context.MentalHealthIssues.Remove(mentalHealthIssues);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MentalHealthIssuesExists(int id)
        {
          return _context.MentalHealthIssues.Any(e => e.Id == id);
        }
    }
}
