using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {

        public CreateCartProfile()
        {
            CreateMap<CreateCartCommand, Cart>();

            CreateMap<CreateCartItemCommand, CartProducts>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom((src, dest, destMember, context) => 
                {
                    var products = context.Items["Products"] as List<Product>;
                    return products?.FirstOrDefault(p => p.Id == src.Id)?.UnitPrice ?? 0;
                }))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom((src, dest, destMember, context) => 
                {
                    var products = context.Items["Products"] as List<Product>;
                    return products?.FirstOrDefault(p => p.Id == src.Id)?.ProductName ?? string.Empty;
                }));

            CreateMap<Cart, CreateCartResult>();
        }
    }
}
