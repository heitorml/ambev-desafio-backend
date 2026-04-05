using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MassTransit;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, DeleteCartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IBus _bus;

        public DeleteCartHandler(ICartRepository cartRepository, IBus bus)
        {
            _cartRepository = cartRepository;
            _bus = bus;
        }

        public async Task<DeleteCartResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCartValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var success = await _cartRepository.DeleteAsync(request.Id, cancellationToken);
            if (!success)
                throw new KeyNotFoundException($"cart with ID {request.Id} not found");

            await SendEvent(request, cancellationToken);

            return new DeleteCartResult { Success = true };
        }

        private async Task SendEvent(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            await _bus.Publish(new SaleCancelledEvent
            {
                CartId = request.Id
            },
            cancellationToken);
        }
    }
}
