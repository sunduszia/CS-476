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
using MyMentalHealth.Observers;


namespace MyMentalHealth.Controllers
{
    public class ContentController : Controller
    {
        private readonly MymentalhealthContext _context;
        //private readonly Contents _contents;

        public ContentController(MymentalhealthContext context)
        {
            _context = context;
            //_contents = new Contents();
            //_contents.Register(new HTMLContentFormatter());

        }
        /*

        //GET: Content/Create
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
                    IssueItemsId = contents.IssueItemsId
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
        */
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

        [Authorize(Roles = "Admin")]
        public IActionResult CreateArticle(int issueItemId, int mentalHealthIssueId)
        {
            ArticleMapping article = new ArticleMapping
            {
                IssueItemsId = issueItemId,
                MentalHealthIssueId = mentalHealthIssueId
            };
            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            ViewBag.IssueItemsId = issueItemId;
            return View(article);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticle([Bind("Title,HTMLContent,IssueItemsId,MentalHealthIssueId")] ArticleMapping article)
        {
            if (ModelState.IsValid)
            {
                Contents newArticle = new Article
                {
                    Title = article.Title,
                    HTMLContent = article.HTMLContent,
                    IssueItemsId = article.IssueItemsId
                };
                //newcontent.IssueItems = await _context.IssueItems.FindAsync(contents.ItemIssueId);

                _context.Add(newArticle);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), "IssueItem", new { mentalHealthIssueId = article.MentalHealthIssueId });
            }
            ViewBag.MentalHealthIssueId = article.MentalHealthIssueId;
            ViewBag.IssueItemsId = article.IssueItemsId;
            return View(article);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateExercise(int issueItemId, int mentalHealthIssueId)
        {
            ExerciseMapping exercise = new ExerciseMapping
            {
                IssueItemsId = issueItemId,
                MentalHealthIssueId = mentalHealthIssueId
            };
            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            ViewBag.IssueItemsId = issueItemId;
            return View(exercise);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExercise([Bind("Title,HTMLContent,VideoLink,IssueItemsId,MentalHealthIssueId")] ExerciseMapping exercise)
        {
            if (ModelState.IsValid)
            {
                Contents newExercise = new Exercise
                {
                    Title = exercise.Title,
                    HTMLContent = exercise.HTMLContent,
                    VideoLink = exercise.VideoLink,
                    IssueItemsId = exercise.IssueItemsId
                };
                //newcontent.IssueItems = await _context.IssueItems.FindAsync(contents.ItemIssueId);

                _context.Add(newExercise);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), "IssueItem", new { mentalHealthIssueId = exercise.MentalHealthIssueId });
            }
            ViewBag.MentalHealthIssueId = exercise.MentalHealthIssueId;
            ViewBag.IssueItemsId = exercise.IssueItemsId;
            return View(exercise);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateDefaultContent(int issueItemId, int mentalHealthIssueId)
        {
            DefaultContentMapping defaultContent = new DefaultContentMapping
            {
                IssueItemsId = issueItemId,
                MentalHealthIssueId = mentalHealthIssueId
            };
            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            ViewBag.IssueItemsId = issueItemId;
            return View(defaultContent);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDefaultContent([Bind("Title,HTMLContent,IssueItemsId,MentalHealthIssueId")] DefaultContentMapping defaultContent)
        {
            if (ModelState.IsValid)
            {
                Contents newDefaultContent = new DefaultContent
                {
                    Title = defaultContent.Title,
                    HTMLContent = defaultContent.HTMLContent,
                    IssueItemsId = defaultContent.IssueItemsId
                };
                //newcontent.IssueItems = await _context.IssueItems.FindAsync(contents.ItemIssueId);

                _context.Add(newDefaultContent);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), "IssueItem", new { mentalHealthIssueId = defaultContent.MentalHealthIssueId });
            }
            ViewBag.MentalHealthIssueId = defaultContent.MentalHealthIssueId;
            ViewBag.IssueItemsId = defaultContent.IssueItemsId;
            return View(defaultContent);
        }

        /*
        GET: Content/Edit/5
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
        
        */
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditArticle(int issueItemId, int mentalHealthIssueId)
        {
            if (issueItemId == 0 || _context.Contents == null)
            {
                return NotFound();
            }

            var contents = await _context.Article.SingleOrDefaultAsync(item => item.IssueItemsId == issueItemId);
            contents.MentalHealthIssueId = mentalHealthIssueId;

            if (contents == null)
            {
                return NotFound();
            }
            ArticleMapping newArticle = new ArticleMapping
            {
                Id = contents.Id,
                Title = contents.Title,
                HTMLContent = contents.HTMLContent,
                IssueItemsId = issueItemId,
                MentalHealthIssueId = mentalHealthIssueId
            };

            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            ViewBag.IssueItemsId = issueItemId;
            return View(newArticle);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(int id, [Bind("Id,Title,HTMLContent,IssueItemsId,MentalHealthIssueId")] ArticleMapping newArticle)
        {
            if (id != newArticle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Contents article = new Article
                    {
                        Id = newArticle.Id,
                        Title = newArticle.Title,
                        HTMLContent = newArticle.HTMLContent,
                        IssueItemsId = newArticle.IssueItemsId
                    };

                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentsExists(newArticle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "IssueItem", new { mentalHealthIssueId = newArticle.MentalHealthIssueId });
            }
            ViewBag.MentalHealthIssueId = newArticle.MentalHealthIssueId;
            ViewBag.IssueItemsId = newArticle.IssueItemsId;
            return View(newArticle);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditExercise(int issueItemId, int mentalHealthIssueId)
        {
            if (issueItemId == 0 || _context.Contents == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercise.SingleOrDefaultAsync(item => item.IssueItemsId == issueItemId);
            exercise.MentalHealthIssueId = mentalHealthIssueId;

            if (exercise == null)
            {
                return NotFound();
            }
            ExerciseMapping newExercise = new ExerciseMapping
            {
                Id = exercise.Id,
                Title = exercise.Title,
                HTMLContent = exercise.HTMLContent,
                VideoLink = exercise.VideoLink,
                IssueItemsId = issueItemId,
                MentalHealthIssueId = mentalHealthIssueId
            };

            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            ViewBag.IssueItemsId = issueItemId;
            return View(newExercise);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditExercise(int id, [Bind("Id,Title,HTMLContent,VideoLink,IssueItemsId,MentalHealthIssueId")] ExerciseMapping newExercise)
        {
            if (id != newExercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Exercise exercise = new Exercise
                    {
                        Id = newExercise.Id,
                        Title = newExercise.Title,
                        HTMLContent = newExercise.HTMLContent,
                        VideoLink = newExercise.VideoLink,
                        IssueItemsId = newExercise.IssueItemsId
                    };

                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentsExists(newExercise.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "IssueItem", new { mentalHealthIssueId = newExercise.MentalHealthIssueId });
            }
            ViewBag.MentalHealthIssueId = newExercise.MentalHealthIssueId;
            ViewBag.IssueItemsId = newExercise.IssueItemsId;
            return View(newExercise);
        }



        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditDefaultContent(int issueItemId, int mentalHealthIssueId)
        {
            if (issueItemId == 0 || _context.Contents == null)
            {
                return NotFound();
            }

            var defaultContents = await _context.DefaultContent.SingleOrDefaultAsync(item => item.IssueItemsId == issueItemId);
            defaultContents.MentalHealthIssueId = mentalHealthIssueId;

            if (defaultContents == null)
            {
                return NotFound();
            }
            DefaultContentMapping newDefaultContent = new DefaultContentMapping
            {
                Id = defaultContents.Id,
                Title = defaultContents.Title,
                HTMLContent = defaultContents.HTMLContent,
                IssueItemsId = issueItemId,
                MentalHealthIssueId = mentalHealthIssueId
            };

            ViewBag.MentalHealthIssueId = mentalHealthIssueId;
            ViewBag.IssueItemsId = issueItemId;
            return View(newDefaultContent);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDefaultContent(int id, [Bind("Id,Title,HTMLContent,IssueItemsId,MentalHealthIssueId")] DefaultContentMapping newDefaultContent)
        {
            if (id != newDefaultContent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Contents defaultContent = new DefaultContent
                    {
                        Id = newDefaultContent.Id,
                        Title = newDefaultContent.Title,
                        HTMLContent = newDefaultContent.HTMLContent,
                        IssueItemsId = newDefaultContent.IssueItemsId
                    };

                    _context.Update(defaultContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentsExists(newDefaultContent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "IssueItem", new { mentalHealthIssueId = newDefaultContent.MentalHealthIssueId });
            }
            ViewBag.MentalHealthIssueId = newDefaultContent.MentalHealthIssueId;
            ViewBag.IssueItemsId = newDefaultContent.IssueItemsId;
            return View(newDefaultContent);
        }
        /*
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
        */
        [Authorize]
        public async Task<IActionResult> ShowArticle(int itemIssueId)
        {
            Article content = await (from item in _context.Article
                                      where item.IssueItemsId == itemIssueId
                                      select new Article
                                      {
                                          Title = item.Title,
                                          HTMLContent = item.HTMLContent
                                      }).FirstOrDefaultAsync();

            return View(content);
        }

        [Authorize]
        public async Task<IActionResult> ShowExercise(int itemIssueId)
        {
            Exercise content = await (from item in _context.Exercise
                                      where item.IssueItemsId == itemIssueId
                                      select new Exercise
                                      {
                                          Title = item.Title,
                                          VideoLink = item.VideoLink,
                                          HTMLContent = item.HTMLContent
                                      }).FirstOrDefaultAsync();

            return View(content);
        }

        [Authorize]
        public async Task<IActionResult> ShowDefault(int itemIssueId)
        {
            DefaultContent content = await (from item in _context.DefaultContent
                                      where item.IssueItemsId == itemIssueId
                                      select new DefaultContent
                                      {
                                          Title = item.Title,
                                          HTMLContent = item.HTMLContent
                                      }).FirstOrDefaultAsync();

            return View(content);
        }
        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
        private bool ExerciseExists(int id)
        {
            return _context.Exercise.Any(e => e.Id == id);
        }
        private bool DefaultContentExists(int id)
        {
            return _context.DefaultContent.Any(e => e.Id == id);
        }
        
        private bool ContentsExists(int id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
