using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hockeyshop.Data.Data.Products;

namespace Hockeyshop.Data.Data.Inventory
{
    public class Stock
    {
        [Key]
        public int IdStock { get; set; }

        [Required(ErrorMessage = "The Product field is required.")]
        [ForeignKey("Product")]
        [Display(Name = "Product ID")]
        public int IdProduct { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Updated At field is required.")]
        [Display(Name = "Update at")]
        public DateTime UpdatedAt { get; set; }
    }
}
