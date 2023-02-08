using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Helpers;
using QLBikeStores.Models;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoresController : Controller
    {

        // GET: Admin/Stores
        public IActionResult Index()
        {
            var stores = Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang);
            return View(stores.ToList());
        }

        // GET: Admin/Stores/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = string.Format(ConstantValues.Store.ChiTietCuaHang, id);
            var store = Utilities.SendDataRequest<Store>(url);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Admin/Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StoreId,StoreName,Phone,Email,Street,City,State,ZipCode")] Store store)
        {
            if (ModelState.IsValid)
            {
                Utilities.SendDataRequest<Store>(ConstantValues.Store.ThemCuaHang, store);
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        // GET: Admin/Stores/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Store.TimKiem, id);
            var store = Utilities.SendDataRequest<Store>(url);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StoreId,StoreName,Phone,Email,Street,City,State,ZipCode")] Store store)
        {
            if (id != store.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Utilities.SendDataRequest<bool>(ConstantValues.Store.CapNhatCuaHang, store);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.StoreId))
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
            return View(store);
        }

        // GET: Admin/Stores/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Store.ChiTietCuaHang, id);
            var store = Utilities.SendDataRequest<Store>(url);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Admin/Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var url = string.Format(ConstantValues.Store.TimKiem, id);
            var store = Utilities.SendDataRequest<Store>(url);
            Utilities.SendDataRequest<bool>(ConstantValues.Store.XoaCuaHang, id);
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            var url = string.Format(ConstantValues.Store.StoreExists, id);
            var store = Utilities.SendDataRequest<bool>(url);
            if (store !=true) return false;
            else return true;
        }
    }
}
