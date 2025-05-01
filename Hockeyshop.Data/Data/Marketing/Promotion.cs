using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Marketing
{
    public class Promotion
    {
        [Key]
        public int IdPromotion { get; set; }

        [Required(ErrorMessage = "The Promotion Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Discount Type field is required.")]
        [ForeignKey("DiscountType")]
        [Display(Name = "Discount type ID")]
        public int IdDiscountType { get; set; }
        public DiscountType DiscountType { get; set; }

        [Required(ErrorMessage = "The Discount Value field is required.")]
        [Display(Name = "Discount value")]
        public decimal DiscountValue { get; set; }

        [Required(ErrorMessage = "The Start Date field is required.")]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The End Date field is required.")]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "The Active field is required.")]
        [Display(Name = "Is it active?")]
        public bool Active { get; set; }

        public ICollection<ProductPromotion> ProductPromotions { get; } = new List<ProductPromotion>();
    }
}
