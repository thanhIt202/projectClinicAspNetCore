using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Controllers;
using Project3.Data;
using Project3.Models;

namespace Project3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersAdminController : BaseAController
    {
        private readonly Sem3DBContext _context;

        public OrdersAdminController(Sem3DBContext context)
        {
            _context = context;
        }

        // GET: Admin/OrdersAdmin
        public async Task<IActionResult> Index()
        {
            var sem3DBContext = _context.Orders.Include(o => o.Account);
            return View(await sem3DBContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdersId,ReceiverName,ReceiverPhone,ReceiverAddress,Note,OrderDate,OrderStatus,AccountId")] Orders orders)
        {
            //if (id != orders.OrdersId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrdersId))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Address", orders.AccountId);
            return View(orders);
        }

     
        private bool OrdersExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrdersId == id)).GetValueOrDefault();
        }
    }
}
