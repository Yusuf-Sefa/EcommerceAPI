
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        // "AsNoTracking" is optional. 
        var category = Q_GetByFilter(c => c.Name == name).AsNoTracking();

        return await category
                        .ProjectTo<ResponseCategoryDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
    public async Task<ResponseCategoryDto?> GetCategoryByCodeAsync(string code)
    {
        // "AsNoTracking" is optional. 
        var category = Q_GetByFilter(c => c.Code == code).AsNoTracking();

        return await category
                        .ProjectTo<ResponseCategoryDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
    public async Task<ResponseCategoryWithProducts?> GetCategoryWithProductsByIdAsync(int id)
    {
        // "AsNoTracking" is optional. 
        var category = Q_GetByFilter(c => c.Id == id).AsNoTracking();

        return await category
                        .ProjectTo<ResponseCategoryWithProducts>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
    public async Task<int?> GetCategoryProductCountByIdAsync(int id)
    {
        var count = await Q_GetByFilter(c => c.Id == id)
                            .Select(c => (int?)c.ProductCategories.Count())
                            .FirstOrDefaultAsync();
        
        return count;
    }

    public async Task<ResponseCategoryDto?> ActivateCategoryByIdAsync(int id)
    {
        var category = await Q_GetByFilter(c => c.Id == id).FirstOrDefaultAsync();

        if (category is null)
            return null;

        category.IsActive = true;
        await E_repository.E_UpdateEntity(category);
        return _mapper.Map<ResponseCategoryDto>(category);
    }
    public async Task<ResponseCategoryDto?> DeactivateCategoryByIdAsync(int id)
    {
        var category = await Q_GetByFilter(c => c.Id == id).FirstOrDefaultAsync();

        if (category is null)
            return null;

        category.IsActive = true;
        await E_repository.E_UpdateEntity(category);
        return _mapper.Map<ResponseCategoryDto>(category);
    }
    
}