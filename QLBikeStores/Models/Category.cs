﻿using System;
using System.Collections.Generic;

#nullable disable

namespace QLBikeStores.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public  ICollection<Product> Products { get; set; }
    }
}
