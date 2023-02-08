using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using X.PagedList;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StocksController : Controller
    {

        // GET: Admin/Stocks
        public IActionResult Index(int? pageNo=1)
        {
            try
            {
                var stocks = Utilities.SendDataRequest<List<Stock>>(ConstantValues.Stock.DocDanhSach);
                var pagedList = stocks.ToPagedList((int)pageNo, 5);
                return View(pagedList);
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Stocks/Details/5
        public IActionResult Details(int? productId,int? storeId)
        {
            try
            {
                if (productId == null || storeId==null)
                {
                    return NotFound();
                }
                var url=string.Format(ConstantValues.Stock.ChiTiet,productId,storeId);

                var stock = Utilities.SendDataRequest<Stock>(url);
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
                ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham), "ProductId", "ProductName");
                ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // POST: Admin/Stocks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StoreId,ProductId,Quantity")] Stock stock)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<Stock>(ConstantValues.Stock.ThemKhoHang, stock);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham), "ProductId", "ProductName", stock.ProductId);
                ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName", stock.StoreId);
                return View(stock);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Stocks/Edit/5
        public IActionResult Edit(int? productId, int? storeId)
        {
            try
            {
                if (productId == null || storeId == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Stock.TimKiem, productId, storeId);

                var stock = Utilities.SendDataRequest<Stock>(url);
                if (stock == null)
                {
                    return NotFound();
                }
                ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham), "ProductId", "ProductName", stock.ProductId);
                ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName", stock.StoreId);
                return View(stock);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // POST: Admin/Stocks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? productId, int? storeId, [Bind("StoreId,ProductId,Quantity")] Stock stock)
        {
            try
            {
                if (storeId != stock.StoreId || productId !=stock.ProductId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.Stock.CapNhatKho, stock);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StockExists(stock.StoreId,stock.ProductId))
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
                ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham), stock.ProductId);
                ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), stock.StoreId);
                return View(stock);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Stocks/Delete/5
        public IActionResult Delete(int? productId, int? storeId)
        {
            try
            {
                if (productId == null || storeId == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Stock.ChiTiet, storeId, productId);
                var stock = Utilities.SendDataRequest<Stock>(url);
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
        public IActionResult DeleteConfirmed(int productId, int storeId)
        {
            try
            {
                var url = string.Format(ConstantValues.Stock.TimKiem, storeId, productId);
                var store = Utilities.SendDataRequest<Stock>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.Stock.XoaKhoHang,store);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
        private bool StockExists(int productId, int storeId)
        {
            var url = string.Format(ConstantValues.Stock.StockExists, storeId, productId);
            var stock = Utilities.SendDataRequest<bool>(url);
            if (stock !=true) return false;
            else return true;
        }
    }
}
