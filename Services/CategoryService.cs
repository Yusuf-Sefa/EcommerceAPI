
using AutoMapper;
using ECommerceAPI.Dtos.CategoryDtos;
using ECommerceAPI.Entities;
using ECommerceAPI.Repository.RepositoryInterfaces;
using ECommerceAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class CategoryService : BaseService<Category, ResponseCategoryDto, CreateCategoryDto, UpdateCategoryDto>, ICategoryService
{
    public CategoryService(IEnumerableRepository<Category> enumerableRepository,
                            IQueryableRepository<Category> queryableRepository,
                            IMapper _mapper,
                            IValidator<CreateCategoryDto> _createValidator,
                            IValidator<UpdateCategoryDto> _updateValidator)
    : base(enumerableRepository,
            queryableRepository,
            _mapper,
            _createValidator,
            _updateValidator)
    { }

    public async Task<ResponseCategoryDto?> GetCategoryByNameAsync(string name)
    {
        var category = await Q_GetByFilter(c => c.Name == name).AsNoTracking().FirstOrDefaultAsync();
        return _mapper.Map<ResponseCategoryDto>(category);
    }
    public async Task<ResponseCategoryDto?> GetCategoryByCodeAsync(string code)
    {
        var category = await Q_GetByFilter(c => c.Code == code).AsNoTracking().FirstOrDefaultAsync();
        return _mapper.Map<ResponseCategoryDto>(category);
    }
    public async Task<ResponseCategoryWithProducts?> GetCategoryWithProductsByIdAsync(int id)
    {
        var category = await Q_GetByFilter(c => c.Id == id)
                            .Include(c => c.ProductCategories)
                            .ThenInclude(pc => pc.Product)
                            .AsNoTrackingWithIdentityResolution()
                            .FirstOrDefaultAsync();

        return _mapper.Map<ResponseCategoryWithProducts>(category);
    }
    public async Task<int> GetCategoryProductCountByIdAsync(int id)
    => await Q_GetByFilter(c => c.Id == id).Select(c => c.ProductCategories.Count()).FirstOrDefaultAsync();

    public async Task<ResponseCategoryDto?> ActivateCategoryByIdAsync(int id)
    {
        var category = await Q_GetByFilter(c => c.Id == id).AsNoTracking().FirstOrDefaultAsync();

        if (category == null)
            return null;

        category.IsActive = true;
        await E_repository.E_UpdateEntity(category);
        return _mapper.Map<ResponseCategoryDto>(category);
    }
    public async Task<ResponseCategoryDto?> DeactivateCategoryByIdAsync(int id)
    {
        var category = await Q_GetByFilter(c => c.Id == id).AsNoTracking().FirstOrDefaultAsync();

        if (category == null)
            return null;

        category.IsActive = true;
        await E_repository.E_UpdateEntity(category);
        return _mapper.Map<ResponseCategoryDto>(category);
    }
    
}