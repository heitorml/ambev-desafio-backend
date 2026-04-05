using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.ListCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts
{
    public class ListCartsProfile : Profile
    {
        public ListCartsProfile()
        {
            CreateMap<ListCartsRequest, ListCartCommand>();
            CreateMap<ListCartResult, ListCartsResponse>();
            CreateMap<UserResult, UserResponse>();
            CreateMap<CartProductResult, CartProductResponse>();
        }
    }
}
