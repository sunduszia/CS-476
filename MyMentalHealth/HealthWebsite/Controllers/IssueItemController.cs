using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Extentions;
using MyMentalHealth.Models;

namespace MyMentalHealth.Controllers
{
    public class IssueItemController : Controller
    {
        private readonly MymentalhealthContext _context;

        public IssueItemController(MymentalhealthContext context)
        {
            _context = context;
        }

        // GET: IssueItem
        public async Task<IActionResult> Index(int mentalHealthIssueId)
        {
            List<IssueItems> listOfIssueItems = await (from issueItem in _context.IssueItems
                                                       join contentItem in _context.Contents
                                                       on issueItem.Id equals contentItem.IssueItems.Id
                                                       into group1
                                                       from subContent in group1.DefaultIfEmpty()
                                                       where issueItem.MentalHealthIssueId == mentalHealthIssueId
                                                       select new IssueItems
                                                       {
                                                           Id = issueItem.Id,
                                                           Title = issueItem.Title,
                                                           Description = issueItem.Description,
                                                           ResourceTypeId = issueItem.ResourceTypeId,
                                                           MentalHealthIssueId = mentalHealthIssueId,
                                                           ContentId = (subContent != null) ? subContent.Id : 0
                                                       }).ToListAsync();



            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            return View(listOfIssueItems);
        }

        // GET: IssueItem/Details/5
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

        // GET: IssueItem/Create
        public async Task<IActionResult> Create(int mentalHealthIssueId)
        {
            List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();
            IssueItems issueItems = new IssueItems
            {
                MentalHealthIssueId = mentalHealthIssueId,
                ResourceTypes = resourceTypes.ConvertToSelectList(0)
            };
            return View(issueItems);
        }

        // POST: IssueItem/Create
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
                return RedirectToAction(nameof(Index), new { mentalHealthIssueId = issueItems.MentalHealthIssueId });
            }
            List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();
            issueItems.ResourceTypes = resourceTypes.ConvertToSelectList(issueItems.ResourceTypeId);
            return View(issueItems);
        }

        // GET: IssueItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IssueItems == null)
            {
                return NotFound();
            }

            List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();

            var issueItems = await _context.IssueItems.FindAsync(id);
            if (issueItems == null)
            {
                return NotFound();
            }
            issueItems.ResourceTypes = resourceTypes.ConvertToSelectList(issueItems.ResourceTypeId);

            return View(issueItems);
        }

        // POST: IssueItem/Edit/5
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
                return RedirectToAction(nameof(Index), new { mentalHealthIssueId = issueItems.MentalHealthIssueId });
            }
            List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();
            issueItems.ResourceTypes = resourceTypes.ConvertToSelectList(issueItems.ResourceTypeId);

            return View(issueItems);
        }
        // GET: IssueItem/Delete/5
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

        // POST: IssueItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IssueItems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IssueItems'  is null.");
            }
            var issueItems = await _context.IssueItems.FindAsync(id);
            if (issueItems != null)
            {
                _context.IssueItems.Remove(issueItems);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { mentalHealthIssueId = issueItems.MentalHealthIssueId });
        }

        private bool IssueItemsExists(int id)
        {
            return (_context.IssueItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
