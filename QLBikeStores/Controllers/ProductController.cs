using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Models;
using System;
using System.Linq;

namespace QLBikeStores.Controllers
{
    public class ProductController : Controller
    {
        private readonly demoContext _context;

        public ProductController(demoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult listProduct()
        {
            try
            {
                var products = _context.Products.ToList();
                return PartialView(products);
            }
            catch (Exception)
            {
                return BadRequest(); 
            }
            
        }
        //public ActionResult ProductCategory(int Id)
        //{
        //    try
        //    {
        //        var products = _context.Products.Where(x=>x.CategoryId==Id).ToList();
        //        return PartialView(products);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }

        //}
        //public ActionResult ChildrenPartial(int choose)
        //{
        //    try
        //    {
        //        if(choose == 1)
        //        {
        //            var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 1).ToList();
        //            return PartialView(electra);
        //        }    
        //        else if(choose == 2)
        //        {
        //            var haro = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 2).ToList();
        //            return PartialView(haro);
        //        }    
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //    return View();
        //}
        public ActionResult ChildrenElectraPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 1).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }            
        }
        public ActionResult ChildrenHaroPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 2).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ChildrenHellerPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 3).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
            
        }
        public ActionResult ChildrenPureCyclesPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 4).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ChildrenRitcheyPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 5).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ChildrenStriderPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 6).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ChildrenSunBicyclesPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 7).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ChildrenSurlyPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 8).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ChildrenTrekPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 1 && n.BrandId == 9).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public ActionResult ComfortElectraPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 1).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ComfortHaroPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 2).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ComfortHellerPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 3).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        public ActionResult ComfortPureCyclesPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 4).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ComfortRitcheyPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 5).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ComfortStriderPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 6).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ComfortSunBicyclesPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 7).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ComfortSurlyPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 8).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public ActionResult ComfortTrekPartial()
        {
            try
            {
                var electra = _context.Products.Where(n => n.CategoryId == 2 && n.BrandId == 9).ToList();
                return PartialView(electra);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        
    }
}
