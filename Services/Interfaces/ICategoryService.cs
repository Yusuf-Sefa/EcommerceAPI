
using ECommerceAPI.Dtos.CategoryDtos;

namespace ECommerceAPI.Services.Interfaces;

public interface ICategoryService
{
    public Task<ResponseCategoryDto?> GetCategoryByNameAsync(string name);
    public Task<ResponseCategoryDto?> GetCategoryByCodeAsync(string code);
    public Task<ResponseCategoryWithProducts?> GetCategoryWithProductsByIdAsync();
    public Task<int> GetCategoryProductCountByIdAsync(int id);

    public Task<ResponseCategoryDto?> ActivateCategoryByIdAsync(int id);
    public Task<ResponseCategoryDto?> DeactivateCategoryByIdAsync(int id);


}