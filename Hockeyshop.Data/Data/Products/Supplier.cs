using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Products
{
    public class Supplier
    {
        [Key]
        public int IdSupplier { get; set; }

        [Required(ErrorMessage = "The Supplier Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Contact Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Contact email")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "The Post Code field is required.")]
        [Display(Name = "Post code")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "The City field is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "The Street and Number field is required.")]
        [Display(Name = "Street and number")]
        public string StreetAndNumber { get; set; }

        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
