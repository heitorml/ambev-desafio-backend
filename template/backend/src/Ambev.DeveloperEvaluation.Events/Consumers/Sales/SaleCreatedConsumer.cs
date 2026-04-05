using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Events.Consumers.Sales
{
    public class SaleCreatedConsumer : IConsumer<SaleCreatedEvent>
    {
        private readonly ILogger<SaleCreatedConsumer> _logger;

        public SaleCreatedConsumer(ILogger<SaleCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SaleCreatedEvent> context)
        {
            var @event = context.Message;
            _logger.LogInformation("Sale Created: {CartId} for User {UserId}", @event?.CartId, @event?.UserId);
            await Task.CompletedTask;
        }
    }
}
