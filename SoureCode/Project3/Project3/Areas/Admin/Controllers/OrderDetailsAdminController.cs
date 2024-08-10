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
    public class OrderDetailsAdminController : BaseAController
    {
        private readonly Sem3DBContext _context;

        public OrderDetailsAdminController(Sem3DBContext context)
        {
            _context = context;
        }

        // GET: Admin/OrderDetailsAdmin
        public async Task<IActionResult> Index(int id)
        {
            var Detail_Orders = _context.OrderDetails.Include(o => o.Orders).Include(o => o.Product).Where(o => o.OrdersId == id);
            var name = await _context.Orders.FirstOrDefaultAsync(a => a.OrdersId == id);
            ViewData["Name"] = name.ReceiverName;
            return View(await Detail_Orders.ToListAsync());
        }

      
        private bool OrderDetailExists(int id)
        {
          return (_context.OrderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}
