
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceAPI.Dtos.BrandDtos;
using ECommerceAPI.Dtos.ProductDtos;
using ECommerceAPI.Entities;
using ECommerceAPI.Repository.RepositoryInterfaces;
using ECommerceAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class ProductService : BaseService<Product, ResponseProductDto, CreateProductDto, UpdateProductDto>
{
    public ProductService(IEnumerableRepository<Product> enumerableRepository,
                          IQueryableRepository<Product> queryableRepository,
                          Mapper _mapper,
                          IValidator<CreateProductDto> _createValidator,
                          IValidator<UpdateProductDto> _updateValidator)
    : base(enumerableRepository,
            queryableRepository,
            _mapper,
            _createValidator,
            _updateValidator)
    { }

    public async Task<ResponseProductDto?> GetProductByName(string name)
    {
        var products = Q_GetByFilter(p => p.Name == name).AsNoTracking();

        return await products
                        .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
    public async Task<List<ResponseProductDto>?> GetProductByBrandName(string brandName)
    {
        var products = Q_GetByFilter(p => p.Brand.Name == brandName).AsNoTrackingWithIdentityResolution();

        return await products
                        .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
    }

    public async Task<List<ResponseProductDto>?> GetProductByPrice(int? minPrice = null, int? maxPrice = null)
    {
        var query = Q_GetAll().AsNoTracking();

        if(minPrice is not null)
            query = query.Where(p => p.Price >= minPrice);

        if(maxPrice is not null)
            query = query.Where(p => p.Price <= maxPrice);
        
        return await query
                        .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();

    }

    public async Task<List<ResponseProductDto>?> GetProductByStock(int? minStock = null, int? maxStock = null)
    {
        var query = Q_GetAll().AsNoTracking();

        if(minStock is not null)
            query = query.Where(p => p.StockQuantity >= minStock);
        
        if(maxStock is not null)
            query = query.Where(p => p.StockQuantity <= maxStock);

        return await query
                        .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();

    }

    public async Task<ResponseProductDto?> DeactiveProductById(int id)
    {
        var product = await Q_GetByFilter(p => p.Id == id)
                            .Include(p => p.Brand)
                            .FirstOrDefaultAsync();

        if(product is null)
            return null;
        
        product.IsActive = false;
        await E_repository.E_UpdateEntity(product);
        return _mapper.Map<ResponseProductDto>(product);
    }
    public async Task<ResponseProductDto?> ActivateProductById(int id)
    {
        var product = await Q_GetByFilter(p => p.Id == id)
                            .Include(p => p.Brand)
                            .FirstOrDefaultAsync();

        if(product is null)
            return null;

        product.IsActive = true;
        await E_repository.E_UpdateEntity(product);
        return _mapper.Map<ResponseProductDto>(product);
    }
    public async Task<ResponseProductDto?> ToggleProductActivationById(int id)
    {
        var product = await Q_GetByFilter(p => p.Id == id)
                            .Include(p => p.Brand)
                            .FirstOrDefaultAsync();

        if(product is null)
            return null;

        product.IsActive = !product.IsActive;
        await E_repository.E_UpdateEntity(product);
        return _mapper.Map<ResponseProductDto>(product);
    }
    public async Task<ResponseProductDto?> ToggleActivationByCategoryName(string name)
    {
        var product = await Q_GetByFilter(p => p.ProductCategories.Any(pc => pc.Category.Name == name))
                            .Include(p => p.Brand)
                            .ToListAsync();

        if(product is null)
            return null;
        foreach(Product p in product)
        {
            p.IsActive = !p.IsActive;
            await E_repository.E_UpdateEntity(p);
        }
        return _mapper.Map<ResponseProductDto>(product);
    }

    public async Task<List<ResponseProductDto>?> GetProductByCategoryId(int id)
    {
        var products = Q_GetByFilter(p => p.ProductCategories.Any(pc => pc.CategoryId == id));
        
        return await products
                        .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
    }
    public async Task<List<ResponseProductDto>?> GetProductByCategoryName(string name)
    {
        var products = Q_GetByFilter(p => p.ProductCategories.Any(pc => pc.Category.Name == name));
                        
        return await products
                        .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
    }

}