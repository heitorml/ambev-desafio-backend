using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts
{
    public class ListProductsProfile : Profile
    {
        public ListProductsProfile()
        {
            CreateMap<ListProductsRequest, ListProductsCommand>()
                .ConstructUsing(src => new ListProductsCommand(src.Id ?? Guid.Empty, src.ProductName ?? string.Empty))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber));
                
            CreateMap<ListProductsResult, ListProductsResponse>();
        }
    }
}
