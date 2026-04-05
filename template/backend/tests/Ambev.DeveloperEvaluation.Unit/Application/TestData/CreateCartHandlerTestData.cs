using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CreateCartHandlerTestData
    {
        private static readonly Faker<CreateCartItemCommand> ItemFaker = new Faker<CreateCartItemCommand>()
            .RuleFor(i => i.Id, f => f.Random.Guid())
            .RuleFor(i => i.Quantity, f => f.Random.Number(1, 20));

        private static readonly Faker<CreateCartCommand> CartFaker = new Faker<CreateCartCommand>()
            .RuleFor(c => c.UserId, f => f.Random.Guid())
            .RuleFor(c => c.Branch, f => f.Company.CompanyName())
            .RuleFor(c => c.Products, f => ItemFaker.Generate(f.Random.Number(1, 5)));

        public static CreateCartCommand GenerateValidCommand()
        {
            return CartFaker.Generate();
        }
    }
}
