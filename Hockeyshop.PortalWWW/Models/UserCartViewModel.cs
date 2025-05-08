namespace Hockeyshop.PortalWWW.Models
{
    public class UserCartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public decimal TotalAmount { get; set; }
        public int ItemCount { get; set; }
    }
}
