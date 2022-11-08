using System;
using System.Collections.Generic;
using System.Data;
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
    public class ContentController : Controller
    {
        private readonly MymentalhealthContext _context;

        public ContentController(MymentalhealthContext context)
        {
            _context = context;
        }




        // GET: Content/Create
        public IActionResult Create(int issueItemId, int mentalHealthIssueId)
        {
            Contents content = new Contents
            {
                ItemIssueId = issueItemId,
                MentalHealthIssueId = mentalHealthIssueId
            };
            return View(content);
        }

        // POST: Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HTMLContent,VideoLink,ItemIssueId,MentalHealthIssueId")] Contents contents)
        {
            if (ModelState.IsValid)
            {
                contents.IssueItems = await _context.IssueItems.FindAsync(contents.ItemIssueId);
                _context.Add(contents);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), "IssueItem", new { mentalHealthIssueId = contents.MentalHealthIssueId });
            }
            return View(contents);
        }

        // GET: Content/Edit/5
        public async Task<IActionResult> Edit(int issueItemId, int mentalHealthIssueId)
        {
            if (issueItemId == 0 || _context.Contents == null)
            {
                return NotFound();
            }

            var contents = await _context.Contents.SingleOrDefaultAsync(item => item.IssueItems.Id == issueItemId);
            contents.MentalHealthIssueId = mentalHealthIssueId;

            if (contents == null)
            {
                return NotFound();
            }
            return View(contents);
        }

        // POST: Content/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink,MentalHealthIssueId")] Contents contents)
        {
            if (id != contents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentsExists(contents.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "IssueItem", new { mentalHealthIssueId = contents.MentalHealthIssueId });
            }
            return View(contents);
        }

        private bool ContentsExists(int id)
        {
          return _context.Contents.Any(e => e.Id == id);
        }
    }
}
