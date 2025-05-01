using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Products
{
    public class ProductImage
    {
        [Key]
        public int IdProductImage { get; set; }

        [Required(ErrorMessage = "The Product field is required.")]
        [ForeignKey("Product")]
        [Display(Name = "Product ID")]
        public int IdProduct { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "The Image URL field is required.")]
        [Display(Name = "Imnage url")]
        public string ImageUrl { get; set; }
    }
}
