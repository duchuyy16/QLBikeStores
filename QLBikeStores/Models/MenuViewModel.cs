using System.Collections.Generic;

namespace QLBikeStores.Models
{
    public class MenuViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        List<Brand> Brands { get; set; }
    }
}
