using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Products
{
    public class ProductCategory
    {
        [Key]
        public int IdProductCategory { get; set; }

        [Required(ErrorMessage = "The Category Name field is required.")]
        public string Name { get; set; }

        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
