using Ambev.DeveloperEvaluation.Domain.Events.Products;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Events.Consumers.Products
{
    public class ProductCreatedConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedConsumer> _logger;

        public ProductCreatedConsumer(ILogger<ProductCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var product = context.Message;
            _logger.LogInformation("Product created: {ProductId} - {Name}", product.ProductId, product.ProductName);
            await Task.CompletedTask;

        }
    }
}
