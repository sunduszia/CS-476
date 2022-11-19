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
    [Authorize]
    public class QuestionnaireController : Controller
    {
        private readonly MymentalhealthContext _context;

        public QuestionnaireController(MymentalhealthContext context)
        {
            _context = context;
        }

        // GET: Questionnaire
        public async Task<IActionResult> Index()
        {
              return View(await _context.IssueItems.ToListAsync());
        }

        // GET: Questionnaire/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IssueItems == null)
            {
                return NotFound();
            }

            var issueItems = await _context.IssueItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueItems == null)
            {
                return NotFound();
            }

            return View(issueItems);
        }

        // GET: Questionnaire/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questionnaire/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,MentalHealthIssueId,ResourceTypeId")] IssueItems issueItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issueItems);
        }

        // GET: Questionnaire/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IssueItems == null)
            {
                return NotFound();
            }

            var issueItems = await _context.IssueItems.FindAsync(id);
            if (issueItems == null)
            {
                return NotFound();
            }
            return View(issueItems);
        }

        // POST: Questionnaire/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,MentalHealthIssueId,ResourceTypeId")] IssueItems issueItems)
        {
            if (id != issueItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueItemsExists(issueItems.Id))
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
            return View(issueItems);
        }

        // GET: Questionnaire/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IssueItems == null)
            {
                return NotFound();
            }

            var issueItems = await _context.IssueItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueItems == null)
            {
                return NotFound();
            }

            return View(issueItems);
        }

        // POST: Questionnaire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IssueItems == null)
            {
                return Problem("Entity set 'MymentalhealthContext.IssueItems'  is null.");
            }
            var issueItems = await _context.IssueItems.FindAsync(id);
            if (issueItems != null)
            {
                _context.IssueItems.Remove(issueItems);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueItemsExists(int id)
        {
          return _context.IssueItems.Any(e => e.Id == id);
        }
    }
}
