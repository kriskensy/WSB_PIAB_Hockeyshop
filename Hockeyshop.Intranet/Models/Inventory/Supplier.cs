using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Intranet.Models.Inventory
{
    public class Supplier
    {
        [Key]
        public int IdSupplier { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Contact Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email format is invalid.")]
        [MaxLength(100, ErrorMessage = "The Email must not exceed 100 characters.")]
        [Display(Name = "Eamil")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "The Phone field is required.")]
        [Phone(ErrorMessage = "The Phone format is invalid.")]
        [MaxLength(20, ErrorMessage = "The Phone must not exceed 20 characters.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The Address field is required.")]
        [MaxLength(200, ErrorMessage = "The Address must not exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The Creation Date field is required.")]
        [Display(Name = "Creation Date")]
        public DateTime CreatedAt { get; set; }

        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
