using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMentalHealth.Models;
using MyMentalHealth.Interface;
using MyMentalHealth.Observers;
using System.Security.AccessControl;

namespace MyMentalHealth.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IssueItemController : Controller
    {
        private readonly MymentalhealthContext _context;
        private readonly IIssueItemsService _issueItemsService;

        //private readonly ConcreteMediator _concreteMediator;
        //private readonly IMediator _mediator;


        public IssueItemController(MymentalhealthContext context, IIssueItemsService issueItemsService)
        {
            _context = context;
            _issueItemsService = issueItemsService;
            //_concreteMediator = concreteMediator;
            //_mediator = mediator;
        }

        // GET: IssueItem
        // public async Task<IActionResult> Index(MentalHealthIssues mentalHealthIssueId)
        public async Task<IActionResult> Index(int mentalHealthIssueId)
        {
            //var tempMentalHealthIssueId = _concreteMediator.Send(mentalHealthIssueId);
            List<IssueItems> listOfIssueItems = await (from issueItem in _context.IssueItems
                                                       join contentItem in _context.Contents
                                                       on issueItem.Id equals contentItem.IssueItemsId
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
                                                           ContentId = (subContent != null) ? subContent.Id : 0,
                                                       }).ToListAsync();
            

            foreach(var item in listOfIssueItems)
            {
                var resource = _context.ResourceTypes.FirstOrDefault(e => e.Id == item.ResourceTypeId);
                item.ResourceTitle = resource.Title;
            }

            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            return View(listOfIssueItems);
        }
        
        //public string GetResourceTitle(int resourceTypeId)
        //{
        //    var resource = _context.ResourceTypes.FirstOrDefault(e => e.Id == resourceTypeId);
        //    return resource.Title;
        //}
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
        /*
        // GET: IssueItem/Create
        public async Task<IActionResult> Create(int mentalHealthIssueId)
        {
            List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();
            IssueItems issueItems = new IssueItems
            {
                MentalHealthIssueId = mentalHealthIssueId,
                ResourceTypes = resourceTypes.ConvertToSelectList(0)
            };
            IssueItemsMapping NewIssueItems = new IssueItemsMapping
            {
                MentalHealthIssueId = mentalHealthIssueId,
                ResourceTypes = resourceTypes
            };
            return View(NewIssueItems);
        }*/
        
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,MentalHealthIssueId,ResourceTypeId")] IssueItemsMapping issueItems)
        {
            if (ModelState.IsValid)
            {
                IssueItems newIssueItems = new IssueItems
                {
                    Title = issueItems.Title,
                    Description = issueItems.Description,
                    MentalHealthIssueId = issueItems.MentalHealthIssueId,
                    ResourceTypeId = issueItems.ResourceTypeId
                };
                List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();
                issueItems.ResourceTypes = resourceTypes;

                _context.Add(newIssueItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "MentalHealthIssue");
            }
            //List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();
            //issueItems.ResourceTypes = resourceTypes.ConvertToSelectList(issueItems.ResourceTypeId);
            //issueItems.ResourceTypes = resourceTypes;
            return View(issueItems);
        }
        */
        public async Task<IActionResult> Create(int mentalHealthIssueId)
        {
            List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();
            IssueItemsMapping issueItems = new IssueItemsMapping
            {
                MentalHealthIssueId = mentalHealthIssueId,
                //ResourceTypes = resourceTypes
            };
            List<SelectListItem> ResourceList = new List<SelectListItem>();
            foreach(var resource in resourceTypes)
            {
                var newitem = new SelectListItem
                {
                    Text = resource.Title,
                    Value = resource.Id.ToString()
                };
                ResourceList.Add(newitem);
            }
            
            ViewBag.ResourceList = ResourceList;
            return View(issueItems);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,MentalHealthIssueId,ResourceTypeId")] IssueItemsMapping issueItems)
        {
            if (ModelState.IsValid)
            {
                IssueItems items = new IssueItems
                {
                    Title = issueItems.Title,
                    Description = issueItems.Description,
                    MentalHealthIssueId = issueItems.MentalHealthIssueId,
                    ResourceTypeId = issueItems.ResourceTypeId
                };

                _context.Add(items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { mentalHealthIssueId = issueItems.MentalHealthIssueId });
            }
            List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();
            //issueItems.ResourceTypes = resourceTypes.ConvertToSelectList(Int32.Parse(issueItems.ResourceTypeId));
            //issueItems.ResourceTypes = resourceTypes;
            List<SelectListItem> ResourceList = new List<SelectListItem>();
            foreach (var resource in resourceTypes)
            {
                var newitem = new SelectListItem
                {
                    Text = resource.Title,
                    Value = resource.Id.ToString()
                };
                ResourceList.Add(newitem);
            }

            ViewBag.ResourceList = ResourceList;
            return View(issueItems);
        }
       
        /*
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
        */
        // GET: IssueItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IssueItems == null)
            {
                return NotFound();
            }
            List<ResourceTypes> resourceTypes = await _context.ResourceTypes.ToListAsync();

            List<SelectListItem> ResourceList = new List<SelectListItem>();
            foreach (var resource in resourceTypes)
            {
                var newitem = new SelectListItem
                {
                    Text = resource.Title,
                    Value = resource.Id.ToString()
                };
                ResourceList.Add(newitem);
            }

            ViewBag.ResourceList = ResourceList;

            var issueItems = await _context.IssueItems.FindAsync(id);

            if (issueItems == null)
            {
                return NotFound();
            }
            IssueItemsMapping issue = new IssueItemsMapping
            {
                Id = issueItems.Id,
                Title = issueItems.Title,
                Description = issueItems.Description,
                MentalHealthIssueId = issueItems.MentalHealthIssueId,
                ResourceTypeId = issueItems.ResourceTypeId
                
            };

            return View(issue);
        }

        // POST: IssueItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,MentalHealthIssueId,ResourceTypeId")] IssueItemsMapping issueItems)
        {
            if (id != issueItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    IssueItems items = new IssueItems
                    {
                        Id = issueItems.Id,
                        Title = issueItems.Title,
                        Description = issueItems.Description,
                        MentalHealthIssueId = issueItems.MentalHealthIssueId,
                        ResourceTypeId = issueItems.ResourceTypeId
                    };

                    _context.Update(items);
                    await _context.SaveChangesAsync();
                    Debug.WriteLine("Attaching Observers...");
                    var editObserver = new EditObserver();
                    _issueItemsService.Register(editObserver);
                    Debug.WriteLine("Updating Issue Item");
                    _issueItemsService.UpdateIssueItem(items);

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

            List<SelectListItem> ResourceList = new List<SelectListItem>();
            foreach (var resource in resourceTypes)
            {
                var newitem = new SelectListItem
                {
                    Text = resource.Title,
                    Value = resource.Id.ToString()
                };
                ResourceList.Add(newitem);
            }

            ViewBag.ResourceList = ResourceList;
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
            Debug.WriteLine("Attaching Observers...");
            var deleteObserver = new DeleteObserver();
            _issueItemsService.Register(deleteObserver);
            Debug.WriteLine("Updating Issue Item");
            _issueItemsService.UpdateIssueItem(issueItems);
            return RedirectToAction(nameof(Index), new { mentalHealthIssueId = issueItems.MentalHealthIssueId });
        }

        private bool IssueItemsExists(int id)
        {
            return (_context.IssueItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
