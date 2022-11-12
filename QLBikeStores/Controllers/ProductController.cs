﻿using Microsoft.AspNetCore.Mvc;
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

        public ActionResult ListProduct()
        {
            try
            {
                var products = _context.Products.ToList();
                return View(products);
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
                var products = _context.Products.Where(x=>x.CategoryId==categoryId).ToList();
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
                var products = _context.Products.Where(x => x.CategoryId == categoryId && x.BrandId == brandId).ToList();
                return View(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
