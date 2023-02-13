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
        public List<ThongKeModel.Output.ThongKeSanPhamTheoTheLoai> ThongKeSanPham()
        {
            var categories = Utilities.SendDataRequest<List<ThongKeModel.Output.ThongKeSanPhamTheoTheLoai>>(ConstantValues.Category.ThongKeSanPhamTheoTheLoai);
            return categories;
        }

       
    }
}
