using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
    public class ListSalesProfile : Profile
    {
        public ListSalesProfile()
        {
            CreateMap<ListSalesRequest, ListSalesCommand>();
            CreateMap<ListSalesResult, ListSalesResponse>();
        }
    }
}
