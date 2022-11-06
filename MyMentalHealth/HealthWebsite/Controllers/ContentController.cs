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
    public class ContentController : Controller
    {
        private readonly MymentalhealthContext _context;

        public ContentController(MymentalhealthContext context)
        {
            _context = context;
        }

        // GET: Content
        public async Task<IActionResult> Index()
        {
              return View(await _context.Contents.ToListAsync());
        }

        // GET: Content/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contents == null)
            {
                return NotFound();
            }

            var contents = await _context.Contents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contents == null)
            {
                return NotFound();
            }

            return View(contents);
        }

        // GET: Content/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Content/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HTMLContent,VideoLink")] Contents contents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contents);
        }

        // GET: Content/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contents == null)
            {
                return NotFound();
            }

            var contents = await _context.Contents.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HTMLContent,VideoLink")] Contents contents)
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
                return RedirectToAction(nameof(Index));
            }
            return View(contents);
        }

        // GET: Content/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contents == null)
            {
                return NotFound();
            }

            var contents = await _context.Contents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contents == null)
            {
                return NotFound();
            }

            return View(contents);
        }

        // POST: Content/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contents == null)
            {
                return Problem("Entity set 'MymentalhealthContext.Contents'  is null.");
            }
            var contents = await _context.Contents.FindAsync(id);
            if (contents != null)
            {
                _context.Contents.Remove(contents);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentsExists(int id)
        {
          return _context.Contents.Any(e => e.Id == id);
        }
    }
}
