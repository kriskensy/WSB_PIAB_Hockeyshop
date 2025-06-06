namespace Hockeyshop.PortalWWW.Models
{
    public class UserCartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public decimal TotalAmount => Items.Sum(i => i.TotalPrice); //obliczne na bieżąco, nie trzeba aktualizować ręcznie koszyka
        public int ItemCount => Items.Sum(i => i.Quantity); //jw
    }
}
