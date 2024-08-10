using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly Sem3DBContext _context;

        public OrderDetailsController(Sem3DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var Detail_Orders = _context.OrderDetails.Include(o => o.Orders).Include(o => o.Product).Where(o => o.OrdersId == id);
            return View(await Detail_Orders.ToListAsync());
        }

        private bool OrderDetailExists(int id)
        {
          return (_context.OrderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}
