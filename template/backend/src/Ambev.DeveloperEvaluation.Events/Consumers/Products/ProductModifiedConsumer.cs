using Ambev.DeveloperEvaluation.Domain.Events.Products;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Events.Consumers.Products
{
    public class ProductModifiedConsumer : IConsumer<ProductModifiedEvent>
    {
        private readonly ILogger<ProductModifiedConsumer> _logger;

        public ProductModifiedConsumer(ILogger<ProductModifiedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ProductModifiedEvent> context)
        {
            var product = context.Message;
            _logger.LogInformation("Product Modified: {ProductId} - {Name}", product.ProductId, product.ProductName);
            await Task.CompletedTask;
        }
    }
}
