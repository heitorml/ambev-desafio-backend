using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class ProductHandlerTestData
    {
        private static readonly Faker<CreateProductCommand> CreateProductFaker = new Faker<CreateProductCommand>()
            .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
            .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 1000))
            .RuleFor(p => p.Quantity, f => f.Random.Number(1, 100));

        private static readonly Faker<UpdateProductCommand> UpdateProductFaker = new Faker<UpdateProductCommand>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
            .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 1000));

        public static CreateProductCommand GenerateValidCreateCommand()
        {
            return CreateProductFaker.Generate();
        }

        public static UpdateProductCommand GenerateValidUpdateCommand()
        {
            return UpdateProductFaker.Generate();
        }
    }
}
