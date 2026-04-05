using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MassTransit;
using Ambev.DeveloperEvaluation.Domain.Events.Products;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, IBus bus)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Product with id {command.Id} not found");

        product.ProductName = command.ProductName;
        product.UnitPrice = command.UnitPrice;

        await _productRepository.UpdateAsync(product, cancellationToken);

        await SendEvent(product, cancellationToken);

        return _mapper.Map<UpdateProductResult>(product);
    }

    private async Task SendEvent(Product product, CancellationToken cancellationToken)
    {
        await _bus.Publish(new ProductModifiedEvent
        {
            CreatedAt = product.CreatedAt,
            ProductId = product.Id,
            ProductName = product.ProductName,
            UnitPrice = product.UnitPrice

        },
        cancellationToken);
    }
}
