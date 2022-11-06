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
    public class StocksController : Controller
    {
        private readonly demoContext _context;

        public StocksController(demoContext context)
        {
            _context = context;
        }

        // GET: Admin/Stocks
        public async Task<IActionResult> Index(int? pageNo=1)
        {
            try
            {
                var demoContext = _context.Stocks.Include(s => s.Product).Include(s => s.Store);
                var pagedList = await demoContext.ToPagedListAsync((int)pageNo, 5);
                return View(pagedList);
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var stock = await _context.Stocks
                    .Include(s => s.Product)
                    .Include(s => s.Store)
                    .FirstOrDefaultAsync(m => m.StoreId == id);
                if (stock == null)
                {
                    return NotFound();
                }

                return View(stock);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Stocks/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
                ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // POST: Admin/Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,ProductId,Quantity")] Stock stock)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(stock);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", stock.ProductId);
                ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", stock.StoreId);
                return View(stock);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var stock = await _context.Stocks.FindAsync(id);
                if (stock == null)
                {
                    return NotFound();
                }
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", stock.ProductId);
                ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", stock.StoreId);
                return View(stock);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // POST: Admin/Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StoreId,ProductId,Quantity")] Stock stock)
        {
            try
            {
                if (id != stock.StoreId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(stock);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StockExists(stock.StoreId))
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
                ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", stock.ProductId);
                ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", stock.StoreId);
                return View(stock);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var stock = await _context.Stocks
                    .Include(s => s.Product)
                    .Include(s => s.Store)
                    .FirstOrDefaultAsync(m => m.StoreId == id);
                if (stock == null)
                {
                    return NotFound();
                }

                return View(stock);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // POST: Admin/Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var stock = await _context.Stocks.FindAsync(id);
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.StoreId == id);
        }
    }
}
