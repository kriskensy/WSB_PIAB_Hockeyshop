using Hockeyshop.Data.Data.Products;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hockeyshop.Data.Data.Marketing
{
    public class ProductPromotion
    {
        public int IdProduct { get; set; }
        [ValidateNever]
        public Product Product { get; set; }

        public int IdPromotion { get; set; }
        [ValidateNever]
        public Promotion Promotion { get; set; }
    }
}
