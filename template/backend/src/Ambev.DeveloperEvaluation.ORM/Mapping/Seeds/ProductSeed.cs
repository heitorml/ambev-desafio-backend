using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Seeds;

public static class ProductSeed
{
    public static void Seed(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                ProductName = "Cerveja Skol Pilsen 350ml",
                UnitPrice = 3.50m,
                Quantity = 1000,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                ProductName = "Cerveja Brahma Chopp 350ml",
                UnitPrice = 4.00m,
                Quantity = 500,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                ProductName = "Cerveja Stela Golden 350ml",
                UnitPrice = 6.00m,
                Quantity = 500,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
