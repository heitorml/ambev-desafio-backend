using Ambev.DeveloperEvaluation.Domain.Events.Users;
using MassTransit;

namespace Ambev.DeveloperEvaluation.Events.Consumers.Users
{
    public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly ILogger<UserRegisteredConsumer> _logger;

        public UserRegisteredConsumer(ILogger<UserRegisteredConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            var user = context.Message;
            _logger.LogInformation("User Registered: {Id} for User {Username}", user?.UserId, user?.Username);
            await Task.CompletedTask;
        }
    }
}
