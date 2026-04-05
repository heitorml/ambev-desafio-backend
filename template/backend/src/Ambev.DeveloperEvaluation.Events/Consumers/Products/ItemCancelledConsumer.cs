using Ambev.DeveloperEvaluation.Domain.Events.Products;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Events.Consumers.Products
{
    public class ItemCancelledConsumer : IConsumer<ItemCancelledEvent>
    {
        private readonly ILogger<ItemCancelledConsumer> _logger;

        public ItemCancelledConsumer(ILogger<ItemCancelledConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ItemCancelledEvent> context)
        {
            var product = context.Message;
            _logger.LogInformation("Product Canceled: {ProductId} ", product.ProductId);
            await Task.CompletedTask;
        }
    }
}
