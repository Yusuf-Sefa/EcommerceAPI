
using ECommerceAPI.Dtos.CategoryDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Services.Interfaces;

public interface ICategoryService : IBaseService<Category, ResponseCategoryDto, CreateCategoryDto, UpdateCategoryDto>
{
    public Task<ResponseCategoryDto?> GetCategoryByNameAsync(string name);
    public Task<ResponseCategoryDto?> GetCategoryByCodeAsync(string code);
    public Task<ResponseCategoryWithProducts?> GetCategoryWithProductsByIdAsync(int id);
    public Task<int?> GetCategoryProductCountByIdAsync(int id);

    public Task<ResponseCategoryDto?> ActivateCategoryByIdAsync(int id);
    public Task<ResponseCategoryDto?> DeactivateCategoryByIdAsync(int id);


}