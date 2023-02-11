namespace QLBikeStores.Models
{
    public class CartItem
    {
        
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal ListPrice { get; set; }
        public int Quantity { get; set; }
        
        //public double Total => (double)(ListPrice - (ListPrice * Quantity *product.Discount/100));

        public double Total => (double)(ListPrice * Quantity);
        
    }
}
