using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Models;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly demoContext _context;

        public RolesController(demoContext context)
        {
            _context = context;
        }

        // GET: Admin/Roles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: Admin/Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Admin/Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleId))
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
            return View(role);
        }

        // GET: Admin/Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool RoleExists(int id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }
        public async Task<IActionResult> Grant()
        {
            try
            {
                var demoContext = _context.Staffs.Include(s => s.Manager).Include(s => s.Store).Include(r=>r.Role);             
                return View(await demoContext.ToListAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> GrantPermission(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var staff = await _context.Staffs.FindAsync(id);
                if (staff == null)
                {
                    return NotFound();
                }
                //ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email", staff.ManagerId);
                //ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", staff.StoreId);
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GrantPermission(int id, [Bind("StaffId,RoleId")] Staff staff)
        {
            try
            {
                if (id != staff.StaffId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var staffss=_context.Staffs.Find(id) ;     
                        staffss.RoleId= staff.RoleId;
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StaffExists(staff.StaffId))
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
                //ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email", staff.ManagerId);
                //ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", staff.StoreId);
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(e => e.StaffId == id);
        }
        //public IActionResult GrantPermission()
        //{

        //    return View();
        //}

        public IActionResult GrantDelete()
        {

            return View();
        }
    }
}
