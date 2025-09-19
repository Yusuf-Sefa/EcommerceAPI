
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
        CreateMap<Brand, GetBrandDto>().ReverseMap();
        CreateMap<Brand, GetBrandWithProductsDto>().ReverseMap();

        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, GetCategoryDto>().ReverseMap();
        CreateMap<Category, GetCategoryWithProducts>().ReverseMap();

        CreateMap<Order, CreateOrderDto>().ReverseMap();
        CreateMap<Order, GetOrderDto>().ReverseMap();
        CreateMap<Order, GetOrderWithProducts>().ReverseMap();

        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, GetProductDto>().ReverseMap();
        CreateMap<Product, GetProductWithCategoriesDto>().ReverseMap();

        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, GetUserDto>().ReverseMap();

    }
}