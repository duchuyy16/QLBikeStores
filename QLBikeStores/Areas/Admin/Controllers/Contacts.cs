using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Contacts : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var contacts = Utilities.SendDataRequest<List<Contact>>(ConstantValues.Contact.DanhSachLienLac);
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
                var url = string.Format(ConstantValues.Contact.ChiTietLienLac, id);
                var contact = Utilities.SendDataRequest<Contact>(url);

                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
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
        public IActionResult Create([Bind("Id,Name,Email,Message")] Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<Contact>(ConstantValues.Contact.ThemLienLac, contact);
                    return RedirectToAction(nameof(Index));
                }
                return View(contact);
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
                var url = string.Format(ConstantValues.Contact.TimKiem, id);
                var contact = Utilities.SendDataRequest<Contact>(url);
                if (contact == null)
                {
                    return NotFound();
                }
                return View(contact);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // POST: Admin/Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Email,Message,CreateAt")] Contact contact)
        {
            try
            {
                if (id != contact.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        Utilities.SendDataRequest<bool>(ConstantValues.Contact.CapNhatLienLac, contact);

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ContactExists(contact.Id))
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
                return View(contact);
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

                var url = string.Format(ConstantValues.Contact.ChiTietLienLac, id);
                var contact = Utilities.SendDataRequest<Contact>(url);
                if (contact == null)
                {
                    return NotFound();
                }

                return View(contact);
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
                var url = string.Format(ConstantValues.Contact.TimKiem, id);
                var contact = Utilities.SendDataRequest<Contact>(url);

                Utilities.SendDataRequest<bool>(ConstantValues.Contact.XoaLienLac, contact);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        private bool ContactExists(int id)
        {
            var url = string.Format(ConstantValues.Contact.ContactExists, id);
            var contact = Utilities.SendDataRequest<bool>(url);
            if (contact != true) return false;
            else return true;
        }
    }
}
