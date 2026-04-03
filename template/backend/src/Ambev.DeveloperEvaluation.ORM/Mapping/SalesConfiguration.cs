using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SalesConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.Quantity).IsRequired(); 
            builder.Property(s => s.TotalSaleAmount).IsRequired();
            builder.Property(s => s.Branch).IsRequired(); ;
            builder.Property(u => u.CreatedAt).IsRequired();
            
            builder.Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.HasMany(s => s.Products)
                .WithMany(p => p.Sales)
                .UsingEntity(j => j.ToTable("SaleProducts"));

            builder.HasOne(s => s.User)
                .WithMany();

        }
    }
}
