
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project3.Controllers;
using Project3.Data;
using Project3.Models;
using X.PagedList;

namespace Project3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesAdminController : BaseAController
    {
        private readonly Sem3DBContext _contextCat;

        public CategoriesAdminController(Sem3DBContext context)
        {
            _contextCat = context;
        }

        // GET: Admin/CategoriesAdmin
        public async Task<IActionResult> Index(string? name, int? page)
        {
            int pageLimit = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var category = await _contextCat.Categories.OrderByDescending(c => c.CategoryId).ToPagedListAsync(pageNumber, pageLimit);

            if (!String.IsNullOrEmpty(name))
            {
                category = await _contextCat.Categories.Where(c => c.CategoryName.Contains(name)).OrderBy(c => c.CategoryId).ToPagedListAsync(pageNumber, pageLimit);
            }
            return View(category);
        }

        // GET: Admin/CategoriesAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _contextCat.Categories == null)
            {
                return NotFound();
            }

            var category = await _contextCat.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Admin/CategoriesAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoriesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryType")] Category category)
        {
            TempData["Message"] = "";
            var categories = _contextCat.Categories.FirstOrDefault(c => c.CategoryName.Equals(category.CategoryName));

            if (categories != null)
            {
                ViewBag.error = "The category name already exists";
                return View();
            }

            if (ModelState.IsValid)
            {
                _contextCat.Add(category);
                await _contextCat.SaveChangesAsync();
                TempData["Message"] = "New category added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/CategoriesAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _contextCat.Categories == null)
            {
                return NotFound();
            }

            var category = await _contextCat.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/CategoriesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("CategoryId,CategoryName,CategoryType")] Category category)
        {
            TempData["Message"] = "";
            var categories = _contextCat.Categories.FirstOrDefault(c => c.CategoryName.Equals(category.CategoryName) && c.CategoryId != category.CategoryId);

            if (categories != null)
            {
                ViewBag.error = "The category name already exists";
                return View();
            }

            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextCat.Update(category);
                    await _contextCat.SaveChangesAsync();
                    TempData["Message"] = "Category update successful";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: Admin/CategoriesAdmin/Delete/?
        public IActionResult Delete(int? id)
        {
            TempData["Message"] = "";
            TempData["MessageError"] = "";
            var product = _contextCat.Products.Where(p => p.CategoryId == id).ToList();

            if (product.Count() > 0)
            {
                TempData["MessageError"] = "Deletion failed because there are products";
                return RedirectToAction(nameof(Index));
            }

            _contextCat.Remove(_contextCat.Categories.Find(id));
            _contextCat.SaveChanges();
            TempData["Message"] = "Category deletion successful";
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int? id)
        {
          return (_contextCat.Categories?.Any(c => c.CategoryId == id)).GetValueOrDefault();
        }
    }
}
