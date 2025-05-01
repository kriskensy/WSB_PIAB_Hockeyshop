using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hockeyshop.Data.Data.Products;

namespace Hockeyshop.Data.Data.Cart
{
    public class CartItem
    {
        [Key]
        public int IdCartItem { get; set; }

        [Required(ErrorMessage = "The User Cart field is required.")]
        [ForeignKey("UserCart")]
        [Display(Name = "User Cart")]
        public int IdUserCart { get; set; }
        public UserCart UserCart { get; set; }

        [Required(ErrorMessage = "The Product field is required.")]
        [ForeignKey("Product")]
        [Display(Name = "Product ID")]
        public int IdProduct { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Unit Price field is required.")]
        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }
    }
}
