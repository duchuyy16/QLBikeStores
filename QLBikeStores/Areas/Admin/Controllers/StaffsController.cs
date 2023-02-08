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
    public class StaffsController : Controller
    {
        // GET: Admin/Staffs
        public IActionResult Index()
        {
            try
            {
                var staffs = Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien);
                return View(staffs.ToList());
            }
            catch(Exception)
            {
                return BadRequest();
            }        
        }

        // GET: Admin/Staffs/Details/5
        public IActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var url = string.Format(ConstantValues.Staff.ChiTietNhanVien, id);

                var staff = Utilities.SendDataRequest<Staff>(url);
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
                ViewData["ManagerId"] = new SelectList(Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien), "StaffId", "Email");
                ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName");
                ViewData["RoleId"] = new SelectList(Utilities.SendDataRequest<List<Role>>(ConstantValues.Role.DanhSachQuyen), "RoleId", "RoleName");
                return View();
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }

        // POST: Admin/Staffs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StaffId,FirstName,LastName,Email,Phone,Active,StoreId,ManagerId,Username,Password,RoleId")] Staff staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<Staff>(ConstantValues.Staff.ThemNhanVienMoi);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ManagerId"] = new SelectList(Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien), "StaffId", "Email", staff.ManagerId);
                ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName", staff.StoreId);
                ViewData["RoleId"] = new SelectList(Utilities.SendDataRequest<List<Role>>(ConstantValues.Role.DanhSachQuyen), "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Staffs/Edit/5
        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Staff.TimKiem, id);
                var staff = Utilities.SendDataRequest<Staff>(url);
                if (staff == null)
                {
                    return NotFound();
                }
                ViewData["ManagerId"] = new SelectList(Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien), "StaffId", "Email", staff.ManagerId);
                ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName", staff.StoreId);
                ViewData["RoleId"] = new SelectList(Utilities.SendDataRequest<List<Role>>(ConstantValues.Role.DanhSachQuyen), "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }

        // POST: Admin/Staffs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StaffId,FirstName,LastName,Email,Phone,Active,StoreId,ManagerId,Username,Password,RoleId")] Staff staff)
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
                        Utilities.SendDataRequest<bool>(ConstantValues.Staff.CapNhatNhanVien, staff);
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
                ViewData["ManagerId"] = new SelectList(Utilities.SendDataRequest<List<Staff>>(ConstantValues.Staff.DanhSachNhanVien), "StaffId", "Email", staff.ManagerId);
                ViewData["StoreId"] = new SelectList(Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang), "StoreId", "StoreName", staff.StoreId);
                ViewData["RoleId"] = new SelectList(Utilities.SendDataRequest<List<Role>>(ConstantValues.Role.DanhSachQuyen), "RoleId", "RoleName", staff.RoleId);
                return View(staff);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET: Admin/Staffs/Delete/5
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var url = string.Format(ConstantValues.Staff.ChiTietNhanVien, id);

                var staff = Utilities.SendDataRequest<Staff>(url);

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
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var url = string.Format(ConstantValues.Staff.TimKiem, id);
                var staff = Utilities.SendDataRequest<Staff>(url);
                Utilities.SendDataRequest<bool>(ConstantValues.Staff.XoaNhanVien, staff);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private bool StaffExists(int id)
        {
            var url = string.Format(ConstantValues.Staff.StaffExists,id);
            var staff = Utilities.SendDataRequest<bool>(url);
            if (staff !=true) return false;
            else return true;
        }
    }
}
