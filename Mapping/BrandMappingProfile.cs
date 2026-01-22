
using AutoMapper;
using ECommerceAPI.Dtos.BrandDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Mapping;

public class BrandMappingProfile : Profile
{
    public BrandMappingProfile()
    {
        CreateMap<CreateBrandDto, Brand>();
        CreateMap<UpdateBrandDto, Brand>();

        CreateMap<Brand, ResponseBrandDto>();
        CreateMap<Brand, ResponseBrandWithProductsDto>()
                .ForMember(dto => dto.ProductsName,
                            entity => entity.MapFrom(src => src.Products
                                                                .Select(p => p.Name)));
    }
}
