
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project3.Controllers;
using Project3.Data;
using Project3.Models;
using X.PagedList;

namespace Project3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsAdminController : BaseAController
    {
        private readonly Sem3DBContext _contextNew;

        public NewsAdminController(Sem3DBContext context)
        {
            _contextNew = context;
        }

        // GET: Admin/NewsAdmin
        public async Task<IActionResult> Index(int? page)
        {
            int pageLimit = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var news = await _contextNew.News.OrderByDescending(n => n.NewsId).ToPagedListAsync(pageNumber, pageLimit);
            return View(news);
        }

        // GET: Admin/NewsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _contextNew.News == null)
            {
                return NotFound();
            }

            var news = await _contextNew.News.FirstOrDefaultAsync(n => n.NewsId == id);

            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // GET: Admin/NewsAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsId,Title,ShortContent,LongContent,NewsDate,NewsImage,NewsType")] News news)
        {
            TempData["Message"] = "";
            var ne = _contextNew.News.FirstOrDefault(n => n.Title.Equals(news.Title));

            if (ne != null)
            {
                ViewBag.error = "The Title already exists";
                return View();
            }

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count() > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var FileName = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        news.NewsImage = FileName;
                    }
                }

                _contextNew.Add(news);
                await _contextNew.SaveChangesAsync();
                TempData["Message"] = "New news added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: Admin/NewsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _contextNew.News == null)
            {
                return NotFound();
            }

            var news = await _contextNew.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: Admin/NewsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("NewsId,Title,ShortContent,LongContent,NewsDate,NewsImage,NewsType")] News news, string? Image)
        {
            TempData["Message"] = "";
            var ne = _contextNew.News.FirstOrDefault(n => n.Title.Equals(news.Title) && n.NewsId != news.NewsId);

            if (ne != null)
            {
                ViewBag.error = "The Title already exists";
                return View();
            }

            if (id != news.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var files = HttpContext.Request.Form.Files;

                    if (files.Count() > 0 && files[0].Length > 0)
                    {
                        var file = files[0];
                        var FileName = file.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            news.NewsImage = FileName;
                        }
                    }
                    else
                    {
                        news.NewsImage = Image;
                    }

                    _contextNew.Update(news);
                    await _contextNew.SaveChangesAsync();
                    TempData["Message"] = "News update successful";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsId))
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
            return View(news);
        }

        // GET: Admin/NewsAdmin/Delete/5
        public IActionResult Delete(int? id)
        {
            TempData["Message"] = "";
            TempData["MessageError"] = "";
            var comment = _contextNew.Comments.Where(c => c.CommentId == id).ToList();

            if (comment.Count() > 0)
            {
                TempData["MessageError"] = "Deletion failed because there are Comments";
                return RedirectToAction(nameof(Index));
            }

            _contextNew.Remove(_contextNew.News.Find(id));
            _contextNew.SaveChanges();
            TempData["Message"] = "News deletion successful";
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int? id)
        {
          return (_contextNew.News?.Any(n => n.NewsId == id)).GetValueOrDefault();
        }
    }
}
