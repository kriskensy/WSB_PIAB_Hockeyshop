using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Orders;
using Hockeyshop.Interfaces.Orders;
using Hockeyshop.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Hockeyshop.Services.Orders
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        public InvoiceService(HockeyshopContext context) : base(context) { }

        public async Task<Invoice> GenerateForOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.IdOrder == orderId);

            if (order == null)
                throw new Exception("Order not found.");

            var existing = await _context.Invoices.FirstOrDefaultAsync(i => i.IdOrder == orderId);
            if (existing != null)
                return existing;

            var invoiceNumber = $"FV-{DateTime.Now:yyyy}-{orderId:D6}";

            var invoice = new Invoice
            {
                IdOrder = orderId,
                InvoiceNumber = invoiceNumber,
                IssueDate = DateTime.Now,
                TotalAmount = order.TotalAmount,
                IdUser = order.IdUser
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<Invoice> GetByIdAsync(int invoiceId)
        {
            return await _context.Invoices
                .Include(i => i.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.IdInvoice == invoiceId);
        }

        public async Task<Invoice> GetByOrderIdAsync(int orderId)
        {
            return await _context.Invoices
                .Include(i => i.Order)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.IdOrder == orderId);
        }

        public async Task<IEnumerable<Invoice>> GetForUserAsync(int userId)
        {
            return await _context.Invoices
                .Where(i => i.IdUser == userId)
                .Include(i => i.Order)
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();
        }

        public async Task<byte[]> GeneratePdfAsync(int invoiceId)
        {
            var invoice = await GetByIdAsync(invoiceId);
            if (invoice == null)
                throw new Exception("Invoice not found.");

            var document = new InvoiceDocument(invoice);
            return document.GeneratePdf();
        }

        public bool Exists(int invoiceId)
        {
            return _context.Invoices.Any(i => i.IdInvoice == invoiceId);
        }
    }
}
