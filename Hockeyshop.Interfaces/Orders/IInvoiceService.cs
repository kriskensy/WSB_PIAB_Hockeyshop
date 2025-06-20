using System.Collections.Generic;
using System.Threading.Tasks;
using Hockeyshop.Data.Data.Orders;

namespace Hockeyshop.Interfaces.Orders
{
    public interface IInvoiceService
    {
        Task<Invoice> GenerateForOrderAsync(int orderId);
        Task<Invoice> GetByIdAsync(int invoiceId);
        Task<Invoice> GetByOrderIdAsync(int orderId);
        Task<IEnumerable<Invoice>> GetForUserAsync(int userId);
        Task<byte[]> GeneratePdfAsync(int invoiceId);
        bool Exists(int invoiceId);
    }
}