using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Entities.EntityConfiguration;

public class ProductItemVariantOptionConfiguration : IEntityTypeConfiguration<ProductItemVariantOption>
{
    public void Configure(EntityTypeBuilder<ProductItemVariantOption> builder)
    {
        builder.HasKey(pivo => new { pivo.ProductItemId, pivo.VariantOptionId });

        builder.HasOne(pivo => pivo.ProductItem)
            .WithMany(pi => pi.VariantOptions)
            .HasForeignKey(pivo => pivo.ProductItemId)
            .IsRequired();

        builder.HasOne(pivo => pivo.VariantOption)
            .WithMany(vo => vo.ProductItems)
            .HasForeignKey(pivo => pivo.VariantOptionId)
            .IsRequired();
    }
}
