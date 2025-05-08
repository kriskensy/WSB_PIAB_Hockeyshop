namespace Hockeyshop.PortalWWW.Models
{
    public class CartItemViewModel
    {
        public int IdCartItem { get; set; }
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}
