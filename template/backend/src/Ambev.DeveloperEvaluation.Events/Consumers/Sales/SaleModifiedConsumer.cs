using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Events.Consumers.Sales
{
    public class SaleModifiedConsumer : IConsumer<SaleModifiedEvent>
    {
        private readonly ILogger<SaleModifiedConsumer> _logger;

        public SaleModifiedConsumer(ILogger<SaleModifiedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SaleModifiedEvent> context)
        {
            var sale = context.Message;
            _logger.LogInformation("Sale Modified: {Id} for User {UserId}", sale?.CartId, sale?.UserId);
            await Task.CompletedTask;
        }
    }
}
