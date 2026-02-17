
using AutoMapper;
using ECommerceAPI.Dtos.ProductDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {   
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();

        CreateMap<Product, ResponseProductDto>();
        CreateMap<Product, ResponseProductWithCategoriesDto>()
                    .ForMember(dto => dto.CategoryNames,
                                entity => entity.MapFrom(src => src.ProductCategories
                                                                .Select(pc => pc.Category.Name)))
                    .ForMember(dto => dto.BrandName,
                                entity => entity.MapFrom(src => src.Brand.Name));
    }
}