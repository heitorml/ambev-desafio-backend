using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCart
{
    public class ListCartProfile : Profile
    {
        public ListCartProfile()
        {
            CreateMap<Cart, ListCartResult>();
            CreateMap<CartProducts, CartProductResult>();
            CreateMap<User, UserResult>();
        }
    }
}
