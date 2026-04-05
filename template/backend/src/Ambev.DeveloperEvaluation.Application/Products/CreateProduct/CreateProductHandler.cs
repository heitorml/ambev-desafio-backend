using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MassTransit;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Domain.Events.Products;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public CreateProductHandler(IProductRepository productRepository, IMapper mapper, IBus bus)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _bus = bus;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = _mapper.Map<Product>(command);

        var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
        var result = _mapper.Map<CreateProductResult>(createdProduct);
        result.Quantity = command.Quantity;
        result.TotalPrice = command.UnitPrice * command.Quantity;

        await SendEvent(createdProduct, cancellationToken);

        return result;
    }

    private async Task SendEvent(Product product, CancellationToken cancellationToken)
    {
        await _bus.Publish(new ProductCreatedEvent
        {
            CreatedAt = product.CreatedAt,
            ProductId = product.Id,
            ProductName = product.ProductName,
            UnitPrice = product.UnitPrice
        },
        cancellationToken);
    }
}
