
using AutoMapper;
using ECommerceAPI.Dtos.BrandDtos;
using ECommerceAPI.Dtos.CategoryDtos;
using ECommerceAPI.Dtos.OrderDtos;
using ECommerceAPI.Dtos.ProductDtos;
using ECommerceAPI.Dtos.UserDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Brand, CreateBrandDto>().ReverseMap();
        CreateMap<Brand, ResponseBrandDto>().ReverseMap();
        CreateMap<Brand, ResponseBrandWithProductsDto>().ReverseMap();

        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, ResponseCategoryDto>().ReverseMap();
        CreateMap<Category, ResponseCategoryWithProducts>().ReverseMap();

        CreateMap<Order, CreateOrderDto>().ReverseMap();
        CreateMap<Order, ResponseOrderDto>().ReverseMap();
        CreateMap<Order, ResponseOrderWithProducts>().ReverseMap();

        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, ResponseProductDto>().ReverseMap();
        CreateMap<Product, ResponseProductWithCategoriesDto>().ReverseMap();

        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, ResponseUserDto>().ReverseMap();

    }
}