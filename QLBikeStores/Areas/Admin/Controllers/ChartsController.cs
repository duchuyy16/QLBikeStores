using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Areas.Admin.ModelsAdmin;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartsController : Controller
    {
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

        [HttpGet]
        public List<ThongKeModel.Output.ThongKeSanPhamTheoTheLoai> ThongKeSanPham()
        {
            var categories = Utilities.SendDataRequest<List<ThongKeModel.Output.ThongKeSanPhamTheoTheLoai>>(ConstantValues.Category.ThongKeSanPhamTheoTheLoai);
            return categories;
        }

       
    }
}
