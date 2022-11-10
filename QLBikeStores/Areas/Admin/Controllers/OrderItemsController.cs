using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Models;
using X.PagedList;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderItemsController : Controller
    {
        private readonly demoContext _context;

        public OrderItemsController(demoContext context)
        {
            _context = context;
        }

        // GET: Admin/OrderItems
        public async Task<IActionResult> Index(int? pageNo = 1)
        {
            var demoContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);
            var pagedList = await demoContext.ToPagedListAsync((int)pageNo, 10);
            return View(pagedList);

        }

        // GET: Admin/OrderItems/Details/5
        public async Task<IActionResult> Details(int? orderId, int? itemId)
        {
            if (orderId == null && itemId == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == orderId && m.ItemId == itemId);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: Admin/OrderItems/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Admin/OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ItemId,ProductId,Quantity,ListPrice,Discount")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: Admin/OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? orderId, int? itemId)
        {
            if (orderId == null || itemId == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems.FindAsync(orderId, itemId);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // POST: Admin/OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int orderId, int itemId, [Bind("OrderId,ItemId,ProductId,Quantity,ListPrice,Discount")] OrderItem orderItem)
        {
            if (orderId != orderItem.OrderId || itemId != orderItem.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.OrderId, orderItem.ItemId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: Admin/OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? orderId, int? itemId)
        {
            if (orderId == null || itemId == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == orderId && m.ItemId == itemId);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: Admin/OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int orderId, int itemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderId, itemId);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int orderId, int itemId)
        {
            return _context.OrderItems.Any(e => e.OrderId == orderId && e.ItemId == itemId);
        }
    }
}
