using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBikeStores.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ListStore()
        {
            try
            {
                var stores = Utilities.SendDataRequest<List<Store>>(ConstantValues.Store.DocDanhSachCuaHang);
                return View(stores);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
