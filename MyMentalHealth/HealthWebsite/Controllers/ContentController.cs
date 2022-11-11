using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;

namespace MyMentalHealth.Controllers
{
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
            ContentMapping content = new ContentMapping
            {
                IssueItemsId = issueItemId,
                MentalHealthIssueId = mentalHealthIssueId
            };
            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            ViewBag.IssueItemsId = issueItemId;
            return View(content);
        }

        // POST: Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,HTMLContent,VideoLink,IssueItemsId,MentalHealthIssueId")] ContentMapping contents)
        {
            if (ModelState.IsValid)
            {
                Contents newcontent = new Contents
                {
                    Title = contents.Title,
                    HTMLContent = contents.HTMLContent,
                    VideoLink = contents.VideoLink,
                    IssueItemsId =  contents.IssueItemsId
                };
                //newcontent.IssueItems = await _context.IssueItems.FindAsync(contents.ItemIssueId);

                _context.Add(newcontent);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), "IssueItem", new { mentalHealthIssueId = contents.MentalHealthIssueId });
            }
            ViewBag.MentalHealthIssueId = contents.MentalHealthIssueId;
            ViewBag.IssueItemsId = contents.IssueItemsId;
            return View(contents);
        }

        /*

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
        */
        // GET: Content/Edit/5
        public async Task<IActionResult> Edit(int issueItemId, int mentalHealthIssueId)
        {
            if (issueItemId == 0 || _context.Contents == null)
            {
                return NotFound();
            }

            var contents = await _context.Contents.SingleOrDefaultAsync(item => item.IssueItemsId == issueItemId);
            contents.MentalHealthIssueId = mentalHealthIssueId;

            if (contents == null)
            {
                return NotFound();
            }
            ContentMapping content = new ContentMapping
            {
                Id = contents.Id,
                Title = contents.Title,
                HTMLContent = contents.HTMLContent,
                VideoLink = contents.VideoLink,
                IssueItemsId = issueItemId,
                MentalHealthIssueId = mentalHealthIssueId
            };
            
            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            ViewBag.IssueItemsId = issueItemId;
            return View(content);
        }

        // POST: Content/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink,IssueItemsId,MentalHealthIssueId")] ContentMapping contents)
        {
            if (id != contents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Contents newContent = new Contents
                    {
                        Id = contents.Id,
                        Title = contents.Title,
                        HTMLContent = contents.HTMLContent,
                        VideoLink = contents.VideoLink,
                        IssueItemsId = contents.IssueItemsId
                    };
                    _context.Update(newContent);
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
            ViewBag.MentalHealthIssueId = contents.MentalHealthIssueId;
            ViewBag.IssueItemsId = contents.IssueItemsId;
            return View(contents);
        }
        public async Task<IActionResult> Show(int itemIssueId)
        {
            Contents content = await (from item in _context.Contents
                                     where item.IssueItemsId == itemIssueId
                                     select new Contents
                                     {
                                         Title = item.Title,
                                         VideoLink = item.VideoLink,
                                         HTMLContent = item.HTMLContent
                                     }).FirstOrDefaultAsync();

            return View(content);
        }
        private bool ContentsExists(int id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
