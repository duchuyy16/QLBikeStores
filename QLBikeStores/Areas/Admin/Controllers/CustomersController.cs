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
    public class CustomersController : Controller
    {
        public IActionResult Index(int? pageNo = 1)
        {
            var customer = Utilities.SendDataRequest<List<Customer>>(ConstantValues.Customer.DanhSachKhachHang);
            var pagedList = customer.ToPagedList((int)pageNo, 10);
            return View(pagedList);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Customer.ChiTietKhachHang, id);
            var customer = Utilities.SendDataRequest<Customer>(url);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerId,FirstName,LastName,Phone,Email,Street,City,State,ZipCode,Username,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                Utilities.SendDataRequest<Customer>(ConstantValues.Customer.ThemKhachHang, customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Customer.TimKiem, id);
            var customer = Utilities.SendDataRequest<Customer>(url);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CustomerId,FirstName,LastName,Phone,Email,Street,City,State,ZipCode,Username,Password")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Utilities.SendDataRequest<bool>(ConstantValues.Customer.CapNhatKhachHang, customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Customer.ChiTietKhachHang, id);
            var customer = Utilities.SendDataRequest<Customer>(url);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var url = string.Format(ConstantValues.Customer.TimKiem, id);
            var customer = Utilities.SendDataRequest<Customer>(url);
            Utilities.SendDataRequest<bool>(ConstantValues.Customer.XoaKhachHang, id);
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            var url = string.Format(ConstantValues.Customer.CustomerExists, id);
            var customer = Utilities.SendDataRequest<bool>(url);
            if (customer !=true) return false;
            else return true;
        }
    }
}
