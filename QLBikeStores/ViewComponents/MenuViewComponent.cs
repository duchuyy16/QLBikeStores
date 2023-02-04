using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QLBikeStores.Helpers;
using QLBikeStores.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBikeStores.ViewComponents
{
    [ViewComponent(Name = "Menu")]
    public class MenuViewComponent : ViewComponent
    {
        //private readonly demoContext _context;

        //public MenuViewComponent(demoContext context)
        //{
        //    _context = context;
        //}

        public IViewComponentResult Invoke()
        {
            var menu = Utilities.SendDataRequest<List<MenuViewModel>>("/api/Menu/GetMenuList");
            //var customer = Utilities.SendDataRequest<CustomerModel>("/api/Customer/ChiTietKhachHang/1");

            return View(menu);
        }
    }
}
