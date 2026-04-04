using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart
{
    public class GetCartProfile : Profile
    {
        public GetCartProfile()
        {
            CreateMap<GetCartRequest, GetCartCommand>();
            CreateMap<GetCartResult, GetCartResponse>();
            CreateMap<CartProductResult, CartProductResponse>();
        }
    }
}
