using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Attributes;
using QLBikeStores.Helpers;
using QLBikeStores.Models;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    [WebAuthorize("admin")]
    public class RolesController : Controller
    {
        // GET: Admin/Roles
        public IActionResult Index()
        {
            var role= Utilities.SendDataRequest<List<Role>>(ConstantValues.Role.DanhSachQuyen);
            return View(role.ToList());
        }

        // GET: Admin/Roles/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = string.Format(ConstantValues.Role.ChiTietQuyen, id);

            var role = Utilities.SendDataRequest<Role>(url);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RoleId,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                Utilities.SendDataRequest<Role>(ConstantValues.Role.ThemQuyen,role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Admin/Roles/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var url = string.Format(ConstantValues.Role.TimKiem, id);
            var role = Utilities.SendDataRequest<Role>(url);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("RoleId,RoleName")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Utilities.SendDataRequest<bool>(ConstantValues.Role.CapNhatQuyen, role);
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var url = string.Format(ConstantValues.Role.ChiTietQuyen, id);

            var role = Utilities.SendDataRequest<Role>(url);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var url = string.Format(ConstantValues.Role.TimKiem, id);
            var role = Utilities.SendDataRequest<Role>(url);
            Utilities.SendDataRequest<bool>(ConstantValues.Role.XoaQuyen,role);
            return RedirectToAction(nameof(Index));
        }
        private bool RoleExists(int id)
        {
            var url = string.Format(ConstantValues.Role.RoleExists, id);
            var role = Utilities.SendDataRequest<bool>(url);
            if (role != true) return false;
            else return true;
        }
        public IActionResult Grant()
        {
            try
            {
                var staff = Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien);
                return View(staff.ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public IActionResult GrantPermission(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.Staff.TimKiem, id);

                var staff = Utilities.SendDataRequest<Staff>(url);
                //var staff = await _context.Staffs.FindAsync(id);
                if (staff == null)
                {
                    return NotFound();
                }
                //ViewData["ManagerId"] = new SelectList(_context.Staffs, "StaffId", "Email", staff.ManagerId);
                //ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", staff.StoreId);
                ViewData["RoleId"] = new SelectList(Utilities.SendDataRequest<List<Role>>(ConstantValues.Role.DanhSachQuyen), "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GrantPermission(int id, [Bind("StaffId,RoleId")] Staff staff)
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
                        var url = string.Format(ConstantValues.Staff.TimKiem, id);
                        var staffss = Utilities.SendDataRequest<Staff>(url);
                        staffss.RoleId = staff.RoleId;
                        Utilities.SendDataRequest<bool>(ConstantValues.Staff.CapNhatNhanVien, staffss);

                        //var staffss=_context.Staffs.Find(id) ;     
                        //staffss.RoleId= staff.RoleId;
                        //await _context.SaveChangesAsync();
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
                ViewData["RoleId"] = new SelectList(Utilities.SendDataRequest<List<Role>>(ConstantValues.Role.DanhSachQuyen), "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool StaffExists(int id)
        {
            var url = string.Format(ConstantValues.Staff.StaffExists, id);
            var staff = Utilities.SendDataRequest<bool>(url);
            if (staff !=true) return false;
            else return true;
        }
        public IActionResult GrantDelete()
        {
            return View();
        }
    }
}
