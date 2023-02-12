using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System;

namespace QLBikeStores.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Contact model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Utilities.SendDataRequest<Contact>(ConstantValues.Contact.ThemLienLac, model);
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
