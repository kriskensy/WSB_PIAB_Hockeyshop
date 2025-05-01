using Hockeyshop.Data.Data.Products;

namespace Hockeyshop.Data.Data.Marketing
{
    public class ProductPromotion
    {
        public int IdProduct { get; set; }
        public Product Product { get; set; }

        public int IdPromotion { get; set; }
        public Promotion Promotion { get; set; }
    }
}
