using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartHandler : IRequestHandler<GetCartCommand, GetCartResult>
    {
        private readonly ICartRepository _cartsRepository;
        private readonly IMapper _mapper;

        public GetCartHandler(ICartRepository cartsRepository, IMapper mapper)
        {
            _cartsRepository = cartsRepository;
            _mapper = mapper;
        }

        public async Task<GetCartResult> Handle(GetCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetCartValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var cart = await _cartsRepository.GetByIdAsync(request.Id, cancellationToken);
            if (cart == null)
                throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

            return _mapper.Map<GetCartResult>(cart);
        }
    }
}
