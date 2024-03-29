﻿using System;
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
    public class BrandsController : Controller
    {

        public IActionResult Index()
        {
            try
            {
                var contacts = Utilities.SendDataRequest<List<Brand>>(ConstantValues.Brand.DanhSachNhanHieu);
                return View(contacts.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.Brand.ChiTietNhanHieu, id);
                var brand = Utilities.SendDataRequest<Brand>(url);

                if (brand == null)
                {
                    return NotFound();
                }

                return View(brand);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BrandId,BrandName")] Brand brand)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<Brand>(ConstantValues.Brand.ThemNhanHieu, brand);
                    return RedirectToAction(nameof(Index));
                }
                return View(brand);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.Brand.TimKiem, id);
                var brand = Utilities.SendDataRequest<Brand>(url);
                if (brand == null)
                {
                    return NotFound();
                }
                return View(brand);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // POST: Admin/Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BrandId,BrandName")] Brand brand)
        {
            try
            {
                if (id != brand.BrandId) //lỗi kh return về trang index
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.Brand.CapNhatNhanHieu, brand);
                        
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BrandExists(brand.BrandId))
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
                return View(brand);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Brand.ChiTietNhanHieu, id);
                var brand = Utilities.SendDataRequest<Brand>(url);
                if (brand == null)
                {
                    return NotFound();
                }

                return View(brand);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.Brand.TimKiem, id);
                var brand = Utilities.SendDataRequest<Brand>(url);

                Utilities.SendDataRequest<bool>(ConstantValues.Brand.XoaNhanHieu, brand);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool BrandExists(int id)
        {
            var url = string.Format(ConstantValues.Brand.BrandExists, id);
            var brand = Utilities.SendDataRequest<bool>(url);
            if (brand != true) return false;
            else return true;
        }
    }
}
