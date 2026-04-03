using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping.Seeds;

public static class UserSeed
{
    public static void Seed(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Username = "admin_user",
            Email = "admin@ambev.com",
            Password = "$2a$11$N9/Xp5iC4N0b6HmVXZ0k/.sS.c9B/4rK2b3m9O6uJ3AOMZgH2wWLy", // fake hash
            Phone = "11999999999",
            Status = UserStatus.Active,
            Role = UserRole.Admin,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}
