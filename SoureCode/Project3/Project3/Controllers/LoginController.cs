using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Controllers
{
    public class LoginController : Controller
    {
        private readonly Sem3DBContext _context;
        public LoginController(Sem3DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login model)
        {
            TempData["MessageError"] = "";

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var checkAccount = _context.Accounts.Where(a => a.AccountStatus == "In force").FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);
                var checkAccount1 = _context.Accounts.FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);

                if (checkAccount != null)
                {
                    HttpContext.Session.Clear();
                    HttpContext.Session.SetInt32("LoginId", checkAccount.AccountId);
                    HttpContext.Session.SetString("LoginName", checkAccount.FullName);
                    HttpContext.Session.SetString("LoginPhone", checkAccount.Phone);
                    HttpContext.Session.SetString("LoginEmail", checkAccount.Email);
                    HttpContext.Session.SetString("LoginAddress", checkAccount.Address);
                    HttpContext.Session.SetString("LoginAvatar", checkAccount.Avatar);
                    HttpContext.Session.SetString("LoginType", checkAccount.AccountType);
                    return RedirectToAction("Index", "Home");
                }
                else if (checkAccount1 != null && checkAccount1.AccountStatus != "In force")
                {
                    HttpContext.Session.SetString("LoginStatus", checkAccount1.AccountStatus);
                }
                else
                {
                    HttpContext.Session.Clear();
                    TempData["MessageError"] = "Login information is incorrect or does not exist";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("AccountId,FullName,Email,Password,Phone,Address,Avatar,AccountStatus,AccountType")] Account account)
        {
            TempData["Message"] = "";
            var accounts = _context.Accounts.FirstOrDefault(a => a.Email.Equals(account.Email));

            if (accounts != null)
            {
                ViewBag.error = "Account Email already exists";
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
                        account.Avatar = FileName;
                    }
                }

                _context.Add(account);
                await _context.SaveChangesAsync();
                TempData["Message"] = "New account added successfully";
                return RedirectToAction("Index", "Login");
            }
            return View(account);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Login");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Account(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Account(int? id, [Bind("AccountId,FullName,Email,Password,Phone,Address,Avatar,AccountStatus,AccountType")] Account account, string? Image)
        {
            TempData["Message"] = "";

            if (id != account.AccountId)
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
                            account.Avatar = FileName;
                        }
                    }
                    else
                    {
                        account.Avatar = Image;
                    }

                    _context.Update(account);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("LoginName", account.FullName);
                    TempData["Message"] = "Product update successful";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Account", "Login");
            }
            return View(account);
        }

        private bool AccountExists(int? id)
        {
            return (_context.Accounts?.Any(a => a.AccountId == id)).GetValueOrDefault();
        }
    }
}