using System;
using System.Collections.Generic;

#nullable disable

namespace QLBikeStores.Models
{
    public partial class OrderItem
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }
        public double Total => (double)((Quantity * ListPrice) - (Quantity * ListPrice * Discount));

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
