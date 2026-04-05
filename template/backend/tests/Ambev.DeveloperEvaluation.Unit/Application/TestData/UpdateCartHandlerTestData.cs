using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class UpdateCartHandlerTestData
    {
        private static readonly Faker<UpdateCartItemCommand> ItemFaker = new Faker<UpdateCartItemCommand>()
            .RuleFor(i => i.Id, f => f.Random.Guid())
            .RuleFor(i => i.Quantity, f => f.Random.Number(1, 20));

        private static readonly Faker<UpdateCartCommand> CartFaker = new Faker<UpdateCartCommand>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.UserId, f => f.Random.Guid())
            .RuleFor(c => c.Branch, f => f.Company.CompanyName())
            .RuleFor(c => c.Products, f => ItemFaker.Generate(f.Random.Number(1, 5)));

        public static UpdateCartCommand GenerateValidCommand()
        {
            return CartFaker.Generate();
        }
    }
}
