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
    public class ResourceTypeController : Controller
    {
        private readonly MymentalhealthContext _context;

        public ResourceTypeController(MymentalhealthContext context)
        {
            _context = context;
        }

        // GET: ResourceType
        public async Task<IActionResult> Index()
        {
              return View(await _context.ResourceTypes.ToListAsync());
        }

        // GET: ResourceType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ResourceTypes == null)
            {
                return NotFound();
            }

            var resourceTypes = await _context.ResourceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceTypes == null)
            {
                return NotFound();
            }

            return View(resourceTypes);
        }

        // GET: ResourceType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResourceType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title")] ResourceTypesMapping resourceTypes)
        {
            if (ModelState.IsValid)
            {
                ResourceTypes newResourceTypes = new ResourceTypes
                {
                    Title = resourceTypes.Title
                };
                _context.Add(newResourceTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resourceTypes);
        }

        // GET: ResourceType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ResourceTypes == null)
            {
                return NotFound();
            }

            var resourceTypes = await _context.ResourceTypes.FindAsync(id);
            
            if (resourceTypes == null)
            {
                return NotFound();
            }
            ResourceTypesMapping newResource = new ResourceTypesMapping
            {
                Id = resourceTypes.Id,
                Title = resourceTypes.Title
            };
            return View(newResource);
        }

        // POST: ResourceType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] ResourceTypesMapping resourceTypes)
        {
            if (id != resourceTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ResourceTypes newResource = new ResourceTypes
                    {
                        Id = resourceTypes.Id,
                        Title = resourceTypes.Title
                    };
                    _context.Update(newResource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceTypesExists(resourceTypes.Id))
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
            return View(resourceTypes);
        }

        // GET: ResourceType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResourceTypes == null)
            {
                return NotFound();
            }

            var resourceTypes = await _context.ResourceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceTypes == null)
            {
                return NotFound();
            }

            return View(resourceTypes);
        }

        // POST: ResourceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResourceTypes == null)
            {
                return Problem("Entity set 'MymentalhealthContext.ResourceTypes'  is null.");
            }
            var resourceTypes = await _context.ResourceTypes.FindAsync(id);
            if (resourceTypes != null)
            {
                _context.ResourceTypes.Remove(resourceTypes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceTypesExists(int id)
        {
          return _context.ResourceTypes.Any(e => e.Id == id);
        }
    }
}
