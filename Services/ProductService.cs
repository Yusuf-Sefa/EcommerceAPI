
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceAPI.Dtos.BrandDtos;
using ECommerceAPI.Dtos.ProductDtos;
using ECommerceAPI.Entities;
using ECommerceAPI.Entities.PivotTables;
using ECommerceAPI.Repository.RepositoryInterfaces;
using ECommerceAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class ProductService : BaseService<Product, ResponseProductDto, CreateProductDto, UpdateProductDto>, IProductService
{


    private readonly IBrandService _brandService;
    private readonly ICategoryService _categoryService;
    
    public ProductService(IEnumerableRepository<Product> enumerableRepository,
                          IQueryableRepository<Product> queryableRepository,
                          IMapper _mapper,
                          IValidator<CreateProductDto> _createValidator,
                          IValidator<UpdateProductDto> _updateValidator,
                          IBrandService brandService,
                          ICategoryService categoryService)
    : base(enumerableRepository,
            queryableRepository,
            _mapper,
            _createValidator,
            _updateValidator)
    {
        _brandService = brandService;
        _categoryService = categoryService;
    }

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
        /*var product = await Q_GetByFilter(p => p.Id == id)
                            .FirstOrDefaultAsync();

        if(product is null)
            return null;

        product.IsActive = !product.IsActive;
        await E_repository.E_UpdateEntity(product);
        return await Q_GetByFilter(p => p.Id == id)
                        .AsNoTracking()
                        .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
        */
                        
        var product = await Q_GetByFilter(p => p.Id == id)
                            .ExecuteUpdateAsync(p => p.SetProperty(p => p.IsActive, p => !p.IsActive));

        if(product == 0)
            return null;

        return await Q_GetByFilter(p => p.Id == id)
                        .AsNoTracking()
                        .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
    public async Task<List<ResponseProductDto>?> ToggleActivationByCategoryName(string name)
    {
        /*var products = await Q_GetByFilter(p => p.ProductCategories.Any(pc => pc.Category.Name == name))
                            .Include(p => p.Brand)
                            .ToListAsync();

        if(products is null || !products.Any())
            return null;
        foreach(Product p in products)
        {
            p.IsActive = !p.IsActive;
            await E_repository.E_UpdateEntity(p);
        }

        var updatedIds = products.Select(p => p.Id).ToList();

        return await Q_GetByFilter(p => updatedIds.Contains(p.Id))
                    .AsNoTracking()
                    .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        */

        /*
        var updatedIds = await Q_GetByFilter(p => p.ProductCategories.Any(pc => pc.Category.Name == name))
                            .Select(p => p.Id)
                            .ToListAsync();

        if (!updatedIds.Any()) return null;

        await Q_GetByFilter(p => updatedIds.Contains(p.Id))
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.IsActive, p => !p.IsActive));

         return await Q_GetByFilter(p => updatedIds.Contains(p.Id))
                    .AsNoTracking()
                    .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        */

        var query = Q_GetByFilter(p => p.ProductCategories.Any(pc => pc.Category.Name == name));

        var products = await query
                            .ExecuteUpdateAsync(p => p.SetProperty(p => p.IsActive, p => !p.IsActive));

        if(products == 0)
            return null;

        return await query
                    .AsNoTracking()
                    .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
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

    public async override Task<ResponseProductDto> E_AddEntity(CreateProductDto dto)
    {
        var validationResponse = await _createValidator.ValidateAsync(dto);
        if(!validationResponse.IsValid)
            throw new ValidationException(validationResponse.Errors.ToString());

        var brandIsExists = await _brandService.IsExists(dto.BrandId);
        if(!brandIsExists)
            throw new Exception("Brand is not Exists");

        foreach(int id in dto.CategoryIds)
        {
            var categoryIsExsists = await _categoryService.IsExists(id);
            if(!categoryIsExsists)
                throw new Exception($"Category with ID {id} not found");
        }

        var product = _mapper.Map<Product>(dto);

        product.ProductCategories = dto.CategoryIds
                                    .Select(categoryId => new ProductCategory
                                        {
                                            CategoryId = categoryId
                                        })
                                    .ToList();

        await E_repository.E_AddEntity(product);

        return await Q_repository
                        .Q_GetByFilter(p => p.Id == product.Id)
                        .AsNoTracking()
                        .ProjectTo<ResponseProductDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync()
                        ?? throw new Exception("Failed to retrieve created product");
        
    }

}