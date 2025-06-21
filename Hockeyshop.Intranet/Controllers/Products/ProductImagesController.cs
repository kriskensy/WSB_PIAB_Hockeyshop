using Hockeyshop.Data.Data.Products;
using Hockeyshop.Interfaces.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hockeyshop.Web.Controllers
{
    public class ProductImagesController : Controller
    {
        private readonly IProductImageService _imageService;

        public ProductImagesController(IProductImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<IActionResult> Index(int productId)
        {
            var images = await _imageService.GetAllAsync(productId);
            ViewBag.ProductId = productId;
            return View("~/Views/Products/ProductImages/Index.cshtml", images);
        }

        //GET
        public IActionResult Create(int productId)
        {
            var model = new ProductImage { IdProduct = productId };
            return View("~/Views/Products/ProductImages/Create.cshtml", model);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, IFormFile image)
        {

            Console.WriteLine($"DEBUG: Otrzymane productId = {productId}");//debug

            if (image != null)
            {
                await _imageService.CreateAsync(productId, image);
                return RedirectToAction(nameof(Index), new { productId });
            }
            ModelState.AddModelError("", "Choose image file.");
            //ViewBag.ProductId = productId;
            var model = new ProductImage { IdProduct = productId };
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
