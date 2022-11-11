using System.Collections.Generic;

namespace QLBikeStores.Models
{
    public class MenuViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
