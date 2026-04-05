using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MassTransit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Products;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IBus _bus;

    public DeleteProductHandler(IProductRepository productRepository, IBus bus)
    {
        _productRepository = productRepository;
        _bus = bus;
    }

    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _productRepository.DeleteAsync(command.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Product with id {command.Id} not found");

        await SendEvent(command.Id, cancellationToken);

        return new DeleteProductResult { Success = true };
    }

    private async Task SendEvent(Guid productId, CancellationToken cancellationToken)
    {
        await _bus.Publish(new ProductCreatedEvent
        {
            ProductId = productId,
        },
        cancellationToken);
    }
}
