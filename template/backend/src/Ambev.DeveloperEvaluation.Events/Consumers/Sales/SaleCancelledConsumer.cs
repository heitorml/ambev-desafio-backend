using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Events.Consumers.Sales
{
    public class SaleCancelledConsumer : IConsumer<SaleCancelledEvent>
    {
        private readonly ILogger<SaleCancelledConsumer> _logger;

        public SaleCancelledConsumer(ILogger<SaleCancelledConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SaleCancelledEvent> context)
        {
            var cart = context.Message;
            _logger.LogInformation("Sale Cancelled: {CartId} for User {UserId}", cart?.CartId, cart?.UserId);
            await Task.CompletedTask;
        }
    }
}
