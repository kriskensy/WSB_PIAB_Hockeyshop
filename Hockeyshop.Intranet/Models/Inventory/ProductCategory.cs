using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Intranet.Models.Inventory
{
    public class ProductCategory
    {
        [Key]
        public int IdCategory { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(50, ErrorMessage = "The Name must not exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [MaxLength(500, ErrorMessage = "The Description must not exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Creation Date field is required.")]
        [Display(Name = "Creation Date")]
        public DateTime CreatedAt { get; set; }

        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
