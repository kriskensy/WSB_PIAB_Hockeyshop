using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Marketing
{
    public class DiscountType
    {
        [Key]
        public int IdDiscountType { get; set; }

        [Required(ErrorMessage = "The Discount Type Name field is required.")]
        [Display(Name = "Discount type")]
        public string DiscountTypeName { get; set; }

        public ICollection<Promotion> Promotions { get; } = new List<Promotion>();
    }
}
