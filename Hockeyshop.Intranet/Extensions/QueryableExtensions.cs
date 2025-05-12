using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.Intranet.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByIdDescending<T>(this IQueryable<T> query) where T : class
        {
            var property = typeof(T).GetProperty("Id") ??
                           typeof(T).GetProperty("IdCartItem") ??
                           typeof(T).GetProperty("IdUserCart") ??
                           typeof(T).GetProperty("IdHockeyNews") ??
                           typeof(T).GetProperty("IdUserRole") ??
                           typeof(T).GetProperty("IdUser") ??
                           typeof(T).GetProperty("IdDiscountType") ??
                           typeof(T).GetProperty("IdProductPromotion") ??
                           typeof(T).GetProperty("IdPromotion") ??
                           typeof(T).GetProperty("IdInvoice") ??
                           typeof(T).GetProperty("IdOrderItem") ??
                           typeof(T).GetProperty("IdOrder") ??
                           typeof(T).GetProperty("IdOrderStatus") ??
                           typeof(T).GetProperty("IdPaymentMethod") ??
                           typeof(T).GetProperty("IdPayment") ??
                           typeof(T).GetProperty("IdPaymentStatus") ??
                           typeof(T).GetProperty("IdProductCategory") ??
                           typeof(T).GetProperty("IdProductImage") ??
                           typeof(T).GetProperty("IdProduct") ??
                           typeof(T).GetProperty("IdSupplier");

            if(property != null)
            {
                string propertyName = property.Name;
                return query.OrderByDescending(item => EF.Property<object>(item, propertyName)); //sortowanie na bazie dzięki EF Core
            }

            return query;
        }
    }
}
