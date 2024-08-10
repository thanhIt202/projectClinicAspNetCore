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
    public class OrderController : Controller
    {
        private readonly Sem3DBContext _context;

        public OrderController(Sem3DBContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var sem3DBContext = _context.Orders.Include(o => o.Account);
           
            return View(await sem3DBContext.ToListAsync());
        }

       

        // GET: Order/Create
        public IActionResult Create()
        {
            var carts = _context.Carts.Include(c => c.Account).Include(p => p.Product);
            int c = 0;
            Int32 a = 0;
            List<Cart> list = new List<Cart>();
            foreach (var item in carts)
            {
                if (item.AccountId == HttpContext.Session.GetInt32("LoginId"))
                {

                    c++;
                    ViewData["Number_Pro"] = c;
                    a += (Int32)item.TotalPrice;
                    ViewData["Total_Cart"] = a.ToString("#,##0 $");

                }

                if (item.AccountId == HttpContext.Session.GetInt32("LoginId"))
                {
                    list.Add(new Cart() { Product = item.Product, TotalPrice = item.TotalPrice, Quantity = item.Quantity });

                }
            }
            ViewData["cart"] = list;
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Address");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdersId,ReceiverName,ReceiverPhone,ReceiverAddress,Note,OrderDate,OrderStatus,AccountId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                orders.OrderDate = DateTime.Now;
                orders.OrderStatus = "Confirming";
                orders.AccountId = HttpContext.Session.GetInt32("LoginId");
                _context.Orders.Add(orders);
                await _context.SaveChangesAsync();
                //var check = true;
                //do
                //{
                    var pro = _context.Carts.Include(c => c.Account).Include(p => p.Product).Where(c => c.AccountId == HttpContext.Session.GetInt32("LoginId"));

                    //if (pro != null)
                    //{
                        foreach (var item in pro)
                        {
                    OrderDetail orderDetail = new OrderDetail();
                            orderDetail.Quantity = item.Quantity;
                            orderDetail.TotalPrice = item.TotalPrice;
                            orderDetail.ProductId = item.ProductId;
                            orderDetail.OrdersId = orders.OrdersId;
                            _context.OrderDetails.Add(orderDetail);
                    await _context.SaveChangesAsync();

                            _context.Remove(_context.Carts.Find(item.CartId));
                    //_context.SaveChanges();
                    await _context.SaveChangesAsync();
                            //break;
                        }
                //}
                //    else
                //    {
                //        check = false;
                //    }

                //} while (check == true);
                return RedirectToAction(nameof(Index));
            }

            var carts = _context.Carts.Include(c => c.Account).Include(p => p.Product);
            int c = 0;
            Int32 a = 0;
            List<Cart> list = new List<Cart>();
            foreach (var item in carts)
            {
                if (item.AccountId == HttpContext.Session.GetInt32("LoginId"))
                {

                    c++;
                    ViewData["Number_Pro"] = c;
                    a += (Int32)item.TotalPrice;
                    ViewData["Total_Cart"] = a.ToString("#,##0 $");

                }

                if (item.AccountId == HttpContext.Session.GetInt32("LoginId"))
                {
                    list.Add(new Cart() { Product = item.Product, TotalPrice = item.TotalPrice, Quantity = item.Quantity });

                }
            }
            ViewData["cart"] = list;
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Address", orders.AccountId);
            return View(orders);
        }

        private bool OrdersExists(int id)
        {
            return (_context.Orders?.Any(e => e.OrdersId == id)).GetValueOrDefault();
        }
    }
}
