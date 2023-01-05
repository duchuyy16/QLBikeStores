using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Areas.Admin.ModelsAdmin;
using QLBikeStores.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartsController : Controller
    {
       
        private readonly demoContext _context;

        public ChartsController(demoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public List<PizzaModel> PopulationChart()
        {
            var model = new List<PizzaModel>
            {
                new PizzaModel{Topping="C1",Slices=3},
                new PizzaModel{Topping="C2",Slices=1},
                new PizzaModel{Topping="C3",Slices=2},
                new PizzaModel{Topping="C4",Slices=4},
                new PizzaModel{Topping="C5",Slices=6},
                new PizzaModel{Topping="C6",Slices=4},
            };
            return model;
        }
        //[HttpGet]
        //public async Task<IActionResult> findAll()
        //{
        //    try
        //    {
        //        var products = _context.Products.ToList();
        //        return Ok(products);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
        //[HttpGet]
        //public ActionResult ListProductCategoryChart(int categoryId)
        //{
        //    var products = _context.Products.Where(x => x.CategoryId == categoryId).Include(p => p.Category).ToList();
        //    return View(products);
        //}

        [HttpGet]
        public List<Category> CategoryChart()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }
    }
}
