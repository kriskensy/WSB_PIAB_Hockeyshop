using Hockeyshop.Data.Data.Products;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Marketing
{
    public class ProductPromotion
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Product")]
        [Display(Name = "Product ID")]
        public int IdProduct { get; set; }
        public Product Product { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Promotion")]
        [Display(Name = "Promotion ID")]
        public int IdPromotion { get; set; }
        public Promotion Promotion { get; set; }
    }
}
