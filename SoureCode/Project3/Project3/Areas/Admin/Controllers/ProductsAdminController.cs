
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Controllers;
using Project3.Data;
using Project3.Models;
using X.PagedList;

namespace Project3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsAdminController : BaseAController
    {
        private readonly Sem3DBContext _contextPro;

        public ProductsAdminController(Sem3DBContext context)
        {
            _contextPro = context;
        }

        // GET: Admin/ProductsAdmin
        public async Task<IActionResult> Index(string? name, int? page)
        {
            int pageLimit = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var product = await _contextPro.Products.Include(c => c.Category).OrderByDescending(p => p.ProductId).ToPagedListAsync(pageNumber, pageLimit);

            if (!String.IsNullOrEmpty(name))
            {
                product = await _contextPro.Products.Include(c => c.Category).Where(p => p.ProductName.Contains(name)).OrderBy(p => p.ProductName).ToPagedListAsync(pageNumber, pageLimit);
            }
            return View(product);
        }

        // GET: Admin/ProductsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _contextPro.Products == null)
            {
                return NotFound();
            }

            var product = await _contextPro.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Admin/ProductsAdmin/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_contextPro.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/ProductsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Price,Description,ProductImage,ProductStatus,CategoryId")] Product product)
        {
            TempData["Message"] = "";
            var products = _contextPro.Products.FirstOrDefault(p => p.ProductName.Equals(product.ProductName));

            if (products != null)
            {
                ViewBag.error = "Product name already exists";
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
                        product.ProductImage = FileName;
                    }
                }
                _contextPro.Add(product);
                await _contextPro.SaveChangesAsync();
                TempData["Message"] = "New product added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_contextPro.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Admin/ProductsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _contextPro.Products == null)
            {
                return NotFound();
            }

            var product = await _contextPro.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_contextPro.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Admin/ProductsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ProductId,ProductName,Price,Description,ProductImage,ProductStatus,CategoryId")] Product product, string? Image)
        {
            TempData["Message"] = "";
            var products = _contextPro.Products.FirstOrDefault(p => p.ProductName.Equals(product.ProductName) && p.ProductId != product.ProductId);

            if (products != null)
            {
                ViewBag.error = "Product name already exists";
                return View();
            }

            if (id != product.ProductId)
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
                            product.ProductImage = FileName;
                        }
                    }
                    else
                    {
                        product.ProductImage = Image;
                    }

                    _contextPro.Update(product);
                    await _contextPro.SaveChangesAsync();
                    TempData["Message"] = "Product update successful";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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

            ViewData["CategoryId"] = new SelectList(_contextPro.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Admin/ProductsAdmin/Delete/5
        public IActionResult Delete(int? id)
        {
            TempData["Message"] = "";
            _contextPro.Remove(_contextPro.Products.Find(id));
            _contextPro.SaveChanges();
            TempData["Message"] = "Product deletion successful";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int? id)
        {
          return (_contextPro.Products?.Any(p => p.ProductId == id)).GetValueOrDefault();
        }
    }
}
