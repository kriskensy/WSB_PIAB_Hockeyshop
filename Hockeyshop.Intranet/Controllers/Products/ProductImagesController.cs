using Hockeyshop.Data.Data.Products;
using Hockeyshop.Interfaces.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hockeyshop.Web.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly IProductImageService _imageService;
        private readonly IProductService _productService;

        public ProductImagesController(IProductImageService imageService, IProductService productService)
        {
            _imageService = imageService;
            _productService = productService;
        }

        public async Task<IActionResult> Index(int productId)
        {
            var images = await _imageService.GetAllAsync(productId);
            ViewBag.ProductId = productId;
            return View("~/Views/Products/ProductImages/Index.cshtml", images);
        }

        //GET
        public async Task<IActionResult> Create(int? productId = null)
        {
            // Pobierz produkty przez serwis
            var products = await _productService.GetAllAsync();
            ViewBag.Products = products
                .Select(p => new SelectListItem
                {
                    Value = p.IdProduct.ToString(),
                    Text = p.Name
                })
                .ToList();

            var model = new ProductImage { IdProduct = productId ?? 0};
            return View("~/Views/Products/ProductImages/Create.cshtml", model);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductImage model, IFormFile image)
        {
            // Pobierz produkty przez serwis (na wypadek błędu walidacji)
            var products = await _productService.GetAllAsync();
            ViewBag.Products = products
                .Select(p => new SelectListItem
                {
                    Value = p.IdProduct.ToString(),
                    Text = p.Name
                })
                .ToList();

            if (image != null)
            {
                await _imageService.CreateAsync(model.IdProduct, image);
                return RedirectToAction(nameof(Index), new { productId = model.IdProduct });
            }
            ModelState.AddModelError("", "Choose image file.");

            return View("~/Views/Products/ProductImages/Create.cshtml", model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
                return NotFound();
            return View("~/Views/Products/ProductImages/Details.cshtml", image);
        }

        //GET
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
                return NotFound();
            return View("~/Views/Products/ProductImages/Delete.cshtml", image);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _imageService.GetByIdAsync(id);
            if (image == null)
                return NotFound();

            int productId = image.IdProduct;
            await _imageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { productId });
        }
    }
}
