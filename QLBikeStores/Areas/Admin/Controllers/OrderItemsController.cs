using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using X.PagedList;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderItemsController : Controller
    {

        // GET: Admin/OrderItems/Details/5
        //them tham so khoa ngoai

        public IActionResult Index(int? pageNo = 1)
        {
            var orderItems = Utilities.SendDataRequest<List<OrderItem>>(ConstantValues.OrderItem.DanhSachDonDatHang);
            var pagedList = orderItems.ToPagedList((int)pageNo, 10);
            return View(pagedList);

        }

        public IActionResult Details(int? orderId, int? itemId)
        {
            if (orderId == null && itemId == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.OrderItem.ChiTietDonDatHang, orderId,itemId);
            var orderItem = Utilities.SendDataRequest<OrderItem>(url);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: Admin/OrderItems/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(Utilities.SendDataRequest<List<Order>>(ConstantValues.Order.DocDanhSachMuaHang), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham), "ProductId", "ProductName");
            return View();
        }

        // POST: Admin/OrderItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderId,ItemId,ProductId,Quantity,ListPrice,Discount")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                Utilities.SendDataRequest<OrderItem>(ConstantValues.OrderItem.ThemChiTietDonDatHang, orderItem);
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(Utilities.SendDataRequest<List<Order>>(ConstantValues.Order.DocDanhSachMuaHang), "OrderId", "OrderId", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham), "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: Admin/OrderItems/Edit/5
        public IActionResult Edit(int? orderId, int? itemId)
        {
            if (orderId == null || itemId == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.OrderItem.TimKiem, orderId, itemId);
            var orderItem = Utilities.SendDataRequest<OrderItem>(url);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(Utilities.SendDataRequest<List<Order>>(ConstantValues.Order.DocDanhSachMuaHang), "OrderId", "OrderId", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham), "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // POST: Admin/OrderItems/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int orderId, int itemId, [Bind("OrderId,ItemId,ProductId,Quantity,ListPrice,Discount")] OrderItem orderItem)
        {
            if (orderId != orderItem.OrderId || itemId != orderItem.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Utilities.SendDataRequest<bool>(ConstantValues.OrderItem.CapNhatChiTietDonHang, orderItem);
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
            ViewData["OrderId"] = new SelectList(Utilities.SendDataRequest<List<Order>>(ConstantValues.Order.DocDanhSachMuaHang), "OrderId", "OrderId", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(Utilities.SendDataRequest<List<Product>>(ConstantValues.Product.DanhSachSanPham), "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: Admin/OrderItems/Delete/5
        public IActionResult Delete(int? orderId, int? itemId)
        {
            if (orderId == null || itemId == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.OrderItem.ChiTietDonDatHang, orderId, itemId);
            var orderItem = Utilities.SendDataRequest<OrderItem>(url);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: Admin/OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int orderId, int itemId)
        {
            var url = string.Format(ConstantValues.OrderItem.TimKiem, orderId, itemId);
            var orderItem = Utilities.SendDataRequest<OrderItem>(url);
            Utilities.SendDataRequest<bool>(ConstantValues.OrderItem.XoaChiTietDonHang, orderItem);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int orderId, int itemId)
        {
            var url = string.Format(ConstantValues.OrderItem.OrderItemExists,orderId,itemId);
            var orderItem = Utilities.SendDataRequest<bool>(url);
            if (orderItem != true) return false;
            else return true;
        }
    }
}
