using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM.Mapping.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.Quantity).IsRequired(); 
            builder.Property(s => s.TotalCartAmount).IsRequired();
            builder.Property(s => s.Branch).IsRequired(); ;
            builder.Property(u => u.CreatedAt).IsRequired();
            
            builder.Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(20);


            builder.HasOne(s => s.User)
                .WithMany();

            builder.HasMany(c => c.Products)
                .WithOne(cp => cp.Cart);

             CartSeed.Seed(builder);
        }
    }
}
