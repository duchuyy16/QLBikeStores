using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Models;
using System;
using System.Linq;
using X.PagedList;

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

        public ActionResult Search(string name)
        {
            var products = _context.Products.Where(m=>m.ProductName.Contains(name)).ToList();
            return View(products);
        }

        public ActionResult ListProduct(int? pageNo=1)
        {
            try
            {
                var products = _context.Products.Include(p => p.Category).Include(m => m.Stocks).OrderBy(l => l.ListPrice).ToList();
                var pagedList = products.ToPagedList((int)pageNo, 9);
                return View(pagedList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        public ActionResult ListCategory(int categoryId)
        {
            try
            {
                var products = _context.Products.Where(x=>x.CategoryId==categoryId).Include(p => p.Category).Include(m => m.Stocks).OrderBy(l => l.ListPrice).ToList();
                return View(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        public ActionResult ListCategoryBrand(int brandId, int categoryId)
        {
            try
            {
                var products = _context.Products.Where(x => x.CategoryId == categoryId && x.BrandId == brandId).Include(p => p.Category).Include(m => m.Stocks).OrderBy(l => l.ListPrice).ToList();
                
                return View(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
    }
}
