using Hockeyshop.Data.Data.Marketing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//dodatkowa klasa konfiguracyjna dla klucza złożonego dla relacji wiele do wielu bo w EF Core nie można tego robić bezpośrednio w plikach encji
namespace Hockeyshop.Data.Data
{
    public class ProductPromotionConfiguration : IEntityTypeConfiguration<ProductPromotion>
    {
        public void Configure(EntityTypeBuilder<ProductPromotion> builder)
        {
            //konfiguracja klucza
            builder.HasKey(pp => new { pp.IdProduct, pp.IdPromotion });

            //konfiguracje relacji
            builder.HasOne(pp => pp.Product).WithMany(p => p.ProductPromotions).HasForeignKey(pp => pp.IdProduct);

            builder.HasOne(pp => pp.Promotion).WithMany(p => p.ProductPromotions).HasForeignKey(pp => pp.IdPromotion);
        }
    }
}
