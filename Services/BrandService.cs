
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceAPI.Dtos.BrandDtos;
using ECommerceAPI.Entities;
using ECommerceAPI.Repository.RepositoryInterfaces;
using ECommerceAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class BrandService : BaseService<Brand, ResponseBrandDto, CreateBrandDto, UpdateBrandDto>, IBrandService
{

    public BrandService(IEnumerableRepository<Brand> enumerableRepository,
                        IQueryableRepository<Brand> queryableRepository,
                        IMapper _mapper,
                        IValidator<CreateBrandDto> _createValidator,
                        IValidator<UpdateBrandDto> _updateValidator)
    : base(enumerableRepository,
            queryableRepository,
            _mapper,
            _createValidator,
            _updateValidator)
    { }

    public async Task<ResponseBrandDto?> GetBrandByNameAsync(string name)
    {
        var brand = Q_GetByFilter(b => b.Name == name).AsNoTracking();

        return await brand
                        .ProjectTo<ResponseBrandDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
    public async Task<ResponseBrandDto?> GetBrandByCodeAsync(string code)
    {
        var brand = Q_GetByFilter(b => b.Code == code).AsNoTracking();

        return await brand
                        .ProjectTo<ResponseBrandDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
    public async Task<ResponseBrandWithProductsDto?> GetBrandWithProductsById(int id)
    {
        var brand = Q_GetByFilter(b => b.Id == id).AsNoTracking();

        return await brand
                        .ProjectTo<ResponseBrandWithProductsDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }
    
    public async Task<int?> GetBrandProductCountAsync(int id)
    {
        var count = await Q_GetByFilter(b => b.Id == id)
                    .Select(b => (int?)b.Products.Count())
                    .FirstOrDefaultAsync(); 
        
        return count;
    }

    public async Task<ResponseBrandDto?> ActivateBrandAsync(int id)
    {
        var brand = await Q_GetByFilter(b => b.Id == id).FirstOrDefaultAsync();

        if (brand == null)
            return null;

        brand.IsActive = true;
        await E_repository.E_UpdateEntity(brand);
        return _mapper.Map<ResponseBrandDto>(brand);
    }
    public async Task<ResponseBrandDto?> DeactivateBrandAsync(int id)
    {
        var brand = await Q_GetByFilter(b => b.Id == id).FirstOrDefaultAsync();

        if (brand == null)
            return null;

        brand.IsActive = false;
        await E_repository.E_UpdateEntity(brand);
        return _mapper.Map<ResponseBrandDto>(brand);
    }

}