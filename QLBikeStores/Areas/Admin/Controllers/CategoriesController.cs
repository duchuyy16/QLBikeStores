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
    public class CategoriesController : Controller
    {
        // GET: Admin/Categories
        //public async Task<IActionResult> Index()
        //{
        //    try
        //    {
        //        return View(await _context.Categories.ToListAsync());
        //    }
        //    catch(Exception)
        //    {
        //        return BadRequest();
        //    }

        //}

        public IActionResult Index()
        {
            try
            {
                var categories = Utilities.SendDataRequest<List<Category>>(ConstantValues.Category.DanhSachTheLoai);
                return View(categories.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET: Admin/Categories/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    try
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var category = await _context.Categories
        //            .FirstOrDefaultAsync(m => m.CategoryId == id);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(category);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }  
        //}

        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var url=string.Format(ConstantValues.Category.ChiTietTheLoai,id);

                var category = Utilities.SendDataRequest<Category>(url);

                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] Category category)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(category);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(category);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryId,CategoryName")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<Category>(ConstantValues.Category.ThemTheLoai,category);
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET: Admin/Categories/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    try
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var category = await _context.Categories.FindAsync(id);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(category);
        //    }catch(Exception)
        //    {
        //        return BadRequest();
        //    }
        //}

        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Category.TimKiem, id);
                var category = Utilities.SendDataRequest<Category>(url);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.Category.CapNhatTheLoai,category);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CategoryExists(category.CategoryId))
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
                return View(category);
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

                var url = string.Format(ConstantValues.Category.ChiTietTheLoai, id);
                var category = Utilities.SendDataRequest<Category>(url);
                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
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
                var url = string.Format(ConstantValues.Category.TimKiem, id);
                var category = Utilities.SendDataRequest<Category>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.Category.XoaTheLoai,id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();

            }
        }

        private bool CategoryExists(int id)
        {
            var url = string.Format(ConstantValues.Category.CategoryExists, id);
            var category = Utilities.SendDataRequest<bool>(url);
            if (category != true) return false;
            else return true;
        }
    }
}
