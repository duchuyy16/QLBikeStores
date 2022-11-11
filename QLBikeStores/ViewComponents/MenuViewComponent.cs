using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBikeStores.Models;
using System.Collections.Generic;
using System.Linq;

namespace QLBikeStores.ViewComponents
{
    [ViewComponent(Name = "Menu")]
    public class MenuViewComponent : ViewComponent
    {
        private readonly demoContext _context;

        public MenuViewComponent(demoContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            List<Category> categories = _context.Categories.ToList();
            List<MenuViewModel> menuList = new List<MenuViewModel>();
            foreach (var category in categories)
            {
                MenuViewModel menu = new MenuViewModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Brands = _context.Brands.Include(x=>x.Products).Where(n=>n.Products.Any(m=>m.CategoryId==category.CategoryId)).ToList()
                };
                menuList.Add(menu);
            }
            return View(menuList);
        }
    }
}
