using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Attributes;
using QLBikeStores.Models;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    [WebAuthorizeAttribute("admin")]
    public class StaffsController : Controller
    {
        private readonly demoContext _context;

        public StaffsController(demoContext context)
        {
            _context = context;
        }

        // GET: Admin/Staffs
        public async Task<IActionResult> Index()
        {
            try
            {
                var demoContext = _context.Staffs.Include(s => s.Manager).Include(s => s.Store);
                return View(await demoContext.ToListAsync());
            }
            catch(Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var staff = await _context.Staffs
                    .Include(s => s.Manager)
                    .Include(s => s.Store)
                    .FirstOrDefaultAsync(m => m.StaffId == id);
                if (staff == null)
                {
                    return NotFound();
                }

                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Staffs/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email");
                ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName");
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }

        // POST: Admin/Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,FirstName,LastName,Email,Phone,Active,StoreId,ManagerId,Username,Password,RoleId")] Staff staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(staff);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email", staff.ManagerId);
                ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", staff.StoreId);
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
                ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email", staff.ManagerId);
                ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", staff.StoreId);
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }

        // POST: Admin/Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,FirstName,LastName,Email,Phone,Active,StoreId,ManagerId,Username,Password,RoleId")] Staff staff)
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
                        _context.Update(staff);
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
                ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email", staff.ManagerId);
                ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", staff.StoreId);
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var staff = await _context.Staffs
                    .Include(s => s.Manager)
                    .Include(s => s.Store)
                    .FirstOrDefaultAsync(m => m.StaffId == id);
                if (staff == null)
                {
                    return NotFound();
                }

                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // POST: Admin/Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var staff = await _context.Staffs.FindAsync(id);
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
    }
}
