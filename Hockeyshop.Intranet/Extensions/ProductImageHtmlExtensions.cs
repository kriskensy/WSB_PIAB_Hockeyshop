using Hockeyshop.Data.Data.Products;

namespace Hockeyshop.Intranet.Extensions
{
    public static class ProductImageHtmlExtensions
    {
        /// <summary>
        /// Generuje HTML <img> dla pierwszego obrazka produktu.
        /// </summary>
        /// <param name="product">Produkt</param>
        /// <param name="portalUrl">Bazowy adres URL serwera PortalWWW</param>
        /// <param name="width">Szerokość obrazka</param>
        /// <param name="height">Wysokość obrazka</param>
        /// <returns>HTML img jako string</returns>
        public static string GetProductImageHtml(this Product product, string portalUrl = "http://localhost:7174", int width = 80, int height = 80)
        {
            var firstImage = product.ProductImages?.FirstOrDefault();
            var imageUrl = firstImage != null && !string.IsNullOrEmpty(firstImage.ImageUrl)
                ? $"{portalUrl}/content/img_products/{firstImage.ImageUrl}"
                : $"https://via.placeholder.com/{width}x{height}?text=No+Image";

            return $"<img src='{imageUrl}' alt='Product image' style='max-width:{width}px;max-height:{height}px;' />";
        }

        public static string GetImageHtml(this ProductImage productImage, string portalUrl = "http://localhost:7174", int width = 80, int height = 80)
        {
            var imageUrl = !string.IsNullOrEmpty(productImage.ImageUrl)
                ? $"{portalUrl}/content/img_products/{productImage.ImageUrl}"
                : $"https://via.placeholder.com/{width}x{height}?text=No+Image";

            return $"<img src='{imageUrl}' alt='Product image' style='max-width:{width}px;max-height:{height}px;' />";
        }
    }
}
