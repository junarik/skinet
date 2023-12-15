using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructue.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.PictureUrl).IsRequired();
        
        // В SQL немає підртимки decimal, тому треба використати було даний метод HasColumnType
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        
        // Для наглядності.
        builder.HasOne(b => b.ProductBrand).WithMany()
            .HasForeignKey(p => p.ProductBrandId);
        
        // (Пояснення) HasOne вказує на те, що наша сутність Product має один t.ProductType
        // А t.ProductType може мати багато інших Products. І HasForeignKey вказує на те,
        // яка саме властивість відповідає за встановлення зв'язку з ProductType
        builder.HasOne(t => t.ProductType).WithMany()
            .HasForeignKey(p => p.ProductTypeId);
    }
}