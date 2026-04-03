using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Seeds;

public static class CartSeed
{
    public static void Seed(EntityTypeBuilder<Cart> builder)
    {
        builder.HasData(new
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Branch = "Filial SP",
            TotalCartAmount = 35.00m,
            Quantity = 10L,
            Status = CartStatus.NotCancelled,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}
