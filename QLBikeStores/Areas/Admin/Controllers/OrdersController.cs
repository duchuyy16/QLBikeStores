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
    public class OrdersController : Controller
    {

        // GET: Admin/Orders
        public IActionResult Index(int? pageNo=1)
        {
            var orders = Utilities.SendDataRequest<List<Order>>(ConstantValues.Order.DocDanhSachMuaHang);
            var pagedList =  orders.ToPagedList((int)pageNo, 10);
            return View(pagedList);
            
            
        }

        // GET: Admin/Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = string.Format(ConstantValues.Order.ChiTietMuaHang,id);
            var order = Utilities.SendDataRequest<Order>(url);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(Utilities.SendDataRequest<List<Customer>>(ConstantValues.Customer.DanhSachKhachHang), "CustomerId", "Email");
            ViewData["StaffId"] = new SelectList(Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien), "StaffId", "Email");
            ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName");
            return View();
        }

        // POST: Admin/Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderId,CustomerId,OrderStatus,OrderDate,RequiredDate,ShippedDate,StoreId,StaffId")] Order order)
        {
            if (ModelState.IsValid)
            {
                Utilities.SendDataRequest<Order>(ConstantValues.Order.ThemDonHang);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(Utilities.SendDataRequest<List<Customer>>(ConstantValues.Customer.DanhSachKhachHang), "CustomerId", "Email", order.CustomerId);
            ViewData["StaffId"] = new SelectList(Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien), "StaffId", "Email", order.StaffId);
            ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName", order.StoreId);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Order.TimKiem, id);
            var order = Utilities.SendDataRequest<Order>(url);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(Utilities.SendDataRequest<List<Customer>>(ConstantValues.Customer.DanhSachKhachHang), "CustomerId", "Email", order.CustomerId);
            ViewData["StaffId"] = new SelectList(Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien), "StaffId", "Email", order.StaffId);
            ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName", order.StoreId);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("OrderId,CustomerId,OrderStatus,OrderDate,RequiredDate,ShippedDate,StoreId,StaffId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Utilities.SendDataRequest<bool>(ConstantValues.Order.CapNhatDonHang);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["CustomerId"] = new SelectList(Utilities.SendDataRequest<List<Customer>>(ConstantValues.Customer.DanhSachKhachHang), "CustomerId", "Email", order.CustomerId);
            ViewData["StaffId"] = new SelectList(Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien), "StaffId", "Email", order.StaffId);
            ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName", order.StoreId);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Order.ChiTietMuaHang, id);
            var order = Utilities.SendDataRequest<Order>(url);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        { 
            var url = string.Format(ConstantValues.Order.TimKiem, id);
            var order = Utilities.SendDataRequest<Order>(url);
            Utilities.SendDataRequest<bool>(ConstantValues.Order.XoaDonHang);
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            var url = string.Format(ConstantValues.Order.OrderExists, id);
            var order = Utilities.SendDataRequest<bool>(url);
            if (order != true) return false;
            else return true;
        }
    }
}
