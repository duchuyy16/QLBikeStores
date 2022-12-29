using Microsoft.AspNetCore.Mvc;
using QLBikeStores.Models;

namespace QLBikeStores.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticsController : Controller
    {
       
        private readonly demoContext _context;

        public StatisticsController(demoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
