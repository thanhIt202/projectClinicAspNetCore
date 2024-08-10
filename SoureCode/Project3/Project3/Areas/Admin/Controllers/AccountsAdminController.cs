
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project3.Controllers;
using Project3.Data;
using Project3.Models;
using X.PagedList;

namespace Project3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsAdminController : BaseAController
    {
        private readonly Sem3DBContext _contextAcc;

        public AccountsAdminController(Sem3DBContext context)
        {
            _contextAcc = context;
        }

        // GET: Admin/AccountsAdmin
        public async Task<IActionResult> Index(string? name, int? page)
        {
            int pageLimit = 2;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var account = await _contextAcc.Accounts.OrderByDescending(a => a.AccountId).ToPagedListAsync(pageNumber, pageLimit);

            if (!String.IsNullOrEmpty(name))
            {
                account = await _contextAcc.Accounts.Where(a => a.FullName.Contains(name)).OrderBy(a => a.AccountId).ToPagedListAsync(pageNumber, pageLimit);
            }
            return View(account);
        }

        // GET: Admin/AccountsAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _contextAcc.Accounts == null)
            {
                return NotFound();
            }

            var account = await _contextAcc.Accounts.FirstOrDefaultAsync(a => a.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // GET: Admin/AccountsAdmin/Create
        

        // GET: Admin/AccountsAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _contextAcc.Accounts == null)
            {
                return NotFound();
            }

            var account = await _contextAcc.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Admin/AccountsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("AccountId,FullName,Email,Password,Phone,Address,Avatar,AccountStatus,AccountType")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextAcc.Update(account);
                    await _contextAcc.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Admin/AccountsAdmin/Delete/5
        
        private bool AccountExists(int? id)
        {
          return (_contextAcc.Accounts?.Any(a => a.AccountId == id)).GetValueOrDefault();
        }
    }
}
