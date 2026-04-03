using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartProductsConfiguration : IEntityTypeConfiguration<CartProducts>
    {
        public void Configure(EntityTypeBuilder<CartProducts> builder)
        {
            builder.ToTable("CartProducts");

            builder.HasKey(cp => cp.Id);
            builder.Property(cp => cp.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(cp => cp.Quantity).IsRequired();
            builder.Property(cp => cp.UnitPrice).IsRequired();
            builder.Property(cp => cp.CartId).IsRequired();
            builder.Property(cp => cp.ProductId).IsRequired();
            builder.Property(cp => cp.UnitPrice).IsRequired();
            builder.Property(cp => cp.TotalPrice).IsRequired();
            builder.Property(cp => cp.Discount);
            builder.Property(cp => cp.ProductName).IsRequired();
            builder.Property(cp => cp.CreatedAt).IsRequired();

            builder.HasOne(cp => cp.Cart)
                .WithMany(c => c.Products);

            builder.HasOne(cp => cp.Product)
                .WithMany();
        }
    }
}
