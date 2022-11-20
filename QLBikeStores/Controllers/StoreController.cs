using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Models;
using System;
using System.Linq;

namespace QLBikeStores.Controllers
{
    public class StoreController : Controller
    {
        private readonly demoContext _context;
        public StoreController(demoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ListStore()
        {
            try
            {
                var stores = _context.Stores.ToList();               
                return View(stores);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
