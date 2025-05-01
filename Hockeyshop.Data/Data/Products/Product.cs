using Hockeyshop.Data.Data.Orders;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hockeyshop.Data.Data.Inventory;
using Hockeyshop.Data.Data.Cart;
using Hockeyshop.Data.Data.Marketing;

namespace Hockeyshop.Data.Data.Products
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }

        [Required(ErrorMessage = "The Product Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Product Category field is required.")]
        [ForeignKey("ProductCategory")]
        [Display(Name = "Product category ID")]
        public int IdProductCategory { get; set; }
        public ProductCategory ProductCategory { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Supplier field is required.")]
        [ForeignKey("Supplier")]
        [Display(Name = "Supplier ID")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        public ICollection<ProductImage> ProductImages { get; } = new List<ProductImage>();
        public ICollection<Stock> Stocks { get; } = new List<Stock>();
        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
        public ICollection<ProductPromotion> ProductPromotions { get; } = new List<ProductPromotion>();
        public ICollection<CartItem> CartItems { get; } = new List<CartItem>();
    }
}
