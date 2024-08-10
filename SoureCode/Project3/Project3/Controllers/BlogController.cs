using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;
using X.PagedList;

namespace Project3.Controllers
{
    public class BlogController : Controller
    {
        private readonly Sem3DBContext _context;

        public BlogController(Sem3DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? type, int? page)
        {
            int pageLimit = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var news = await _context.News.OrderByDescending(n => n.NewsId).ToPagedListAsync(pageNumber, pageLimit);

            if (!String.IsNullOrEmpty(type))
            {
                news = await _context.News.Where(n => n.NewsType.Contains(type)).OrderByDescending(n => n.NewsId).ToPagedListAsync(pageNumber, pageLimit);
            }
            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, int? page)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FirstOrDefaultAsync(n => n.NewsId == id);
            int pageLimit = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var comm = await _context.Comments.Include(n => n.News).Include(a => a.Account).Where(n => n.NewsId == id).OrderByDescending(c => c.CommentId).ToPagedListAsync(pageNumber, pageLimit);

            if (news == null)
            {
                return NotFound();
            }
            ViewData["ne_Image"] = news.NewsImage;
            ViewData["ne_title"] = news.Title;
            ViewData["ne_ShortContent"] = news.ShortContent;
            ViewData["ne_LongContent"] = news.LongContent;
            ViewData["ne_id"] = id;
            return View(comm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,CommentDate,NewsId,AccountId")] Comment comment, int? NewsId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Details);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            _context.Remove(_context.Comments.Find(id));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
