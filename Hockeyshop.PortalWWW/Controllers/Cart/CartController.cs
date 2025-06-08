using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Payments;
using Hockeyshop.PortalWWW.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hockeyshop.PortalWWW.Controllers.Cart
{
    public class CartController : BaseController
    {
        private readonly HockeyshopContext _context;

        public CartController(HockeyshopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var cart = GetCartFromSession();
            return View(cart);
        }

        public async Task<IActionResult> Add(int productId)
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var cart = GetCartFromSession();

            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.IdProduct == productId);

            if (product == null)
            {
                TempData["CartError"] = "Product not found.";
                return RedirectToAction("Index", "Shop");
            }

            var item = cart.Items.FirstOrDefault(i => i.IdProduct == productId);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Items.Add(new CartItemViewModel
                {
                    IdProduct = product.IdProduct,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            SaveCartToSession(cart);

            TempData["CartMessage"] = $"{product.Name} added to cart.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int productId)
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var cart = GetCartFromSession();
            cart.Items.RemoveAll(i => i.IdProduct == productId);
            SaveCartToSession(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Dictionary<int, int> quantities) //update ilości
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var cart = GetCartFromSession();
            foreach (var q in quantities)
            {
                var item = cart.Items.FirstOrDefault(i => i.IdProduct == q.Key);
                if (item != null)
                {
                    if (q.Value > 0)
                        item.Quantity = q.Value;
                    else
                        cart.Items.Remove(item);
                }
            }
            SaveCartToSession(cart);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Clear()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }

        //GET: Checkout form
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var cart = GetCartFromSession();

            if(!cart.Items.Any())
            {
                TempData["CartError"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            var paymentMethods = await _context.PaymentMethods
                .OrderBy(pm => pm.Name)
                .Select(pm => new PaymentMethodOption
                {
                    Id = pm.IdPaymentMethod,
                    Name = pm.Name
                }).ToListAsync();

            var model = new CheckoutViewModel
            {
                Cart = cart,
                PaymentMethods = paymentMethods
            };

            return View(model);
        }

        //POST: Process checkout
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            model.Cart = GetCartFromSession(); //ładuje koszyk

            if (!ModelState.IsValid)
            {
                model.PaymentMethods = await _context.PaymentMethods
                    .OrderBy(pm => pm.Name)
                    .Select(pm => new PaymentMethodOption
                    {
                        Id = pm.IdPaymentMethod,
                        Name = pm.Name
                    }).ToListAsync();

                return View(model);
            }

            //zapis metody do sesji
            HttpContext.Session.SetInt32("SelectedPaymentMethod", model.SelectedPaymentMethodId);

            return RedirectToAction("ProcessOrder");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ProcessOrder()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            var cart = GetCartFromSession();
            if (!cart.Items.Any())
            {
                TempData["CartError"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            var selectedPaymentMethod = HttpContext.Session.GetInt32("SelectedPaymentMethod") ?? 1;

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var order = new Hockeyshop.Data.Data.Orders.Order //zamówienie
            {
                IdUser = userId,
                IdOrderStatus = 1, //New - każde zamówienie tworzone z tym statusem
                OrderDate = DateTime.Now,
                TotalAmount = cart.TotalAmount
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in cart.Items) //dodaje pozycje
            {
                var orderItem = new Hockeyshop.Data.Data.Orders.OrderItem
                {
                    IdOrder = order.IdOrder,
                    IdProduct = item.IdProduct,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                };
                _context.OrderItems.Add(orderItem);
            }

            var payment = new Payment
            {
                IdOrder = order.IdOrder,
                PaymentDate = DateTime.Now,
                IdPaymentMethod = selectedPaymentMethod, //wybrana metoda
                IdPaymentStatus = 1, //pending
                Amount = cart.TotalAmount
            };
            _context.Payments.Add(payment);

            var invoiceNumber = await GenerateInvoiceNumber(); //najpierw generownie nr faktury

            var invoice = new Hockeyshop.Data.Data.Orders.Invoice //faktura
            {
                IdOrder = order.IdOrder,
                IdUser = userId,
                IssueDate = DateTime.Now,
                InvoiceNumber = invoiceNumber,
                TotalAmount = cart.TotalAmount
            };
            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync();

            //czyszczenie sesji
            HttpContext.Session.Remove("Cart");
            HttpContext.Session.Remove("SelectedPaymentMethod");

            TempData["CartMessage"] = "Order placed successfully!";
            return RedirectToAction("OrderConfirmation");
        }

        public async Task<IActionResult> OrderConfirmation()
        {
            ViewBag.ProductCategories = await _context.ProductCategories.OrderBy(c => c.Name).ToListAsync();

            return View();
        }

        //helpersy
        private UserCartViewModel GetCartFromSession()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cartJson)
                ? new UserCartViewModel()
                : System.Text.Json.JsonSerializer.Deserialize<UserCartViewModel>(cartJson);
        }

        private void SaveCartToSession(UserCartViewModel cart)
        {
            HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(cart));
        }

        private async Task<string> GenerateInvoiceNumber()
        {
            var year = DateTime.Now.Year;
            var lastInvoice = await _context.Invoices
                .Where(i => i.InvoiceNumber.StartsWith($"INV-{year}-"))
                .OrderByDescending(i => i.IdInvoice)
                .FirstOrDefaultAsync();

            int nextNumber = 1;
            if (lastInvoice != null)
            {
                var lastNumberPart = lastInvoice.InvoiceNumber.Split('-').Last();
                if (int.TryParse(lastNumberPart, out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"INV-{year}-{nextNumber:D3}"; //INV-2025-001
        }

    }
}

