using AutoMapper;
using ProductsApi.Dtos;
using ProductsApi.Entities;

namespace ProductsApi.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
    }
}
