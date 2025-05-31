using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Products;
using Hockeyshop.Intranet.Extensions;
using Hockeyshop.Intranet.Models;
using Hockeyshop.Intranet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.Intranet.Controllers.Products
{
    public class ProductImagesController : Controller
    {
        private readonly HockeyshopContext _context;

        public ProductImagesController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index(string searchTerm)
        {
            var query = _context.ProductImages.Include(item => item.Product).ThenInclude(item => item.ProductCategory).AsQueryable();

            //wyszukiwanie w rekordach tabeli
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(item =>
                    item.Product.Name.Contains(searchTerm) ||
                    item.Product.ProductCategory.Name.Contains(searchTerm) ||
                    item.ImageUrl.Contains(searchTerm));
            }

            //użycie extension do sortowania tabel po id desc
            query = query.OrderByIdDescending();

            var model = await query.ToListAsync();
            ViewBag.SearchTerm = searchTerm;

            return View("~/Views/Products/ProductImages/Index.cshtml", model);
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.IdProductImage == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View("~/Views/Products/ProductImages/Details.cshtml", productImage);
        }

        // GET: ProductImages/Create
        public IActionResult Create()
        {
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Name");
            return View("~/Views/Products/ProductImages/Create.cshtml", new ProductImageUploadViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductImageUploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Name", model.IdProduct);
                //return View(model);
                return View("~/Views/Products/ProductImages/Create.cshtml", model);
            }

            var uploadsPath = Path.Combine("Hockeyshop.PortalWWW", "wwwroot", "content", "img_products");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(model.ImageFile.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ImageFile.CopyToAsync(fileStream);
            }

            var productImage = new ProductImage
            {
                IdProduct = model.IdProduct,
                ImageUrl = fileName
            };

            _context.Add(productImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductImages/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var productImage = await _context.ProductImages.FindAsync(id);
        //    if (productImage == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Name", productImage.IdProduct);
        //    return View("~/Views/Products/ProductImages/Edit.cshtml", productImage);
        //}

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            // Mapowanie ProductImage na ProductImageUploadViewModel
            var viewModel = new ProductImageUploadViewModel
            {
                IdProductImage = productImage.IdProductImage,
                IdProduct = productImage.IdProduct,
                ImageUrl = productImage.ImageUrl // dla podglądu obecnego obrazka
                                                 // ImageFile pozostaje null - użytkownik może wybrać nowy plik
            };

            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Name", productImage.IdProduct);
            return View("~/Views/Products/ProductImages/Edit.cshtml", viewModel);
        }


        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdProductImage,IdProduct,ImageUrl")] ProductImage productImage, IFormFile ImageFile)
        //{
        //    if (id != productImage.IdProductImage)
        //        return NotFound();

        //    if (ImageFile != null && ImageFile.Length > 0)
        //    {
        //        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Hockeyshop.PortalWWW", "wwwroot", "content", "img_products");
        //        if (!Directory.Exists(uploadsPath))
        //            Directory.CreateDirectory(uploadsPath);

        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
        //        var filePath = Path.Combine(uploadsPath, fileName);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await ImageFile.CopyToAsync(fileStream);
        //        }

        //        productImage.ImageUrl = fileName;
        //        ModelState.Remove(nameof(productImage.ImageUrl));
        //    }

        //    if (string.IsNullOrEmpty(productImage.ImageUrl))
        //    {
        //        ModelState.AddModelError(nameof(productImage.ImageUrl), "You must upload an image or provide a URL.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(productImage);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductImageExists(productImage.IdProductImage))
        //                return NotFound();
        //            else
        //                throw;
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Name", productImage.IdProduct);
        //    return View("~/Views/Products/ProductImages/Edit.cshtml", productImage);
        //}

        // POST: ProductImages/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, ProductImageUploadViewModel model)
        //{
        //    if (id != model.IdProductImage)
        //    {
        //        return NotFound();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Name", model.IdProduct);
        //        return View("~/Views/Products/ProductImages/Edit.cshtml", model);
        //    }

        //    try
        //    {
        //        var productImage = await _context.ProductImages.FindAsync(id);
        //        if (productImage == null)
        //        {
        //            return NotFound();
        //        }

        //        // Jeśli przesłano nowy plik, zapisz go i zaktualizuj ImageUrl
        //        if (model.ImageFile != null && model.ImageFile.Length > 0)
        //        {
        //            var uploadsPath = Path.Combine("Hockeyshop.PortalWWW", "wwwroot", "content", "img_products");
        //            if (!Directory.Exists(uploadsPath))
        //                Directory.CreateDirectory(uploadsPath);

        //            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(model.ImageFile.FileName);
        //            var filePath = Path.Combine(uploadsPath, fileName);

        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await model.ImageFile.CopyToAsync(fileStream);
        //            }

        //            // Usuń stary plik (opcjonalnie)
        //            if (!string.IsNullOrEmpty(productImage.ImageUrl))
        //            {
        //                var oldFilePath = Path.Combine(uploadsPath, productImage.ImageUrl);
        //                if (System.IO.File.Exists(oldFilePath))
        //                {
        //                    System.IO.File.Delete(oldFilePath);
        //                }
        //            }

        //            productImage.ImageUrl = fileName;
        //        }

        //        // Zaktualizuj inne właściwości
        //        productImage.IdProduct = model.IdProduct;

        //        _context.Update(productImage);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductImageExists(model.IdProductImage))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductImageUploadViewModel model)
        {
            if (id != model.IdProductImage)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "Name", model.IdProduct);
                return View("~/Views/Products/ProductImages/Edit.cshtml", model);
            }

            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
                return NotFound();

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsPath = Path.Combine("Hockeyshop.PortalWWW", "wwwroot", "content", "img_products");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var fileName = Guid.NewGuid() + "_" + Path.GetFileName(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                // (opcjonalnie) usuń stary plik
                if (!string.IsNullOrEmpty(productImage.ImageUrl))
                {
                    var oldFilePath = Path.Combine(uploadsPath, productImage.ImageUrl);
                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);
                }

                productImage.ImageUrl = fileName;
            }

            productImage.IdProduct = model.IdProduct;

            _context.Update(productImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.IdProductImage == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View("~/Views/Products/ProductImages/Delete.cshtml", productImage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var productImage = await _context.ProductImages.FindAsync(id);
                if (productImage != null)
                {
                    // Usuń fizyczny plik
                    if (!string.IsNullOrEmpty(productImage.ImageUrl))
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Hockeyshop.PortalWWW", "wwwroot", "content", "img_products", productImage.ImageUrl);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    _context.ProductImages.Remove(productImage);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = "This record cannot be deleted because there are related records in other tables!"
                });
            }
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImages.Any(e => e.IdProductImage == id);
        }
    }
}
