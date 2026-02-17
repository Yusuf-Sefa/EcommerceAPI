
using AutoMapper;
using ECommerceAPI.Dtos.CategoryDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Mapping;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();

        CreateMap<Category, ResponseCategoryDto>();
        CreateMap<Category, ResponseCategoryWithProducts>()
                    .ForMember(dto => dto.ProductNames,
                                entity => entity.MapFrom(src => src.ProductCategories
                                                                    .Select(pc => pc.Product.Name)));
    }
}