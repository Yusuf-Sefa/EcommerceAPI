
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
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
        var brand = await Q_GetByFilter(b => b.Name == name).AsNoTracking().FirstOrDefaultAsync();
        return _mapper.Map<ResponseBrandDto>(brand);
    }
    public async Task<ResponseBrandDto?> GetBrandByCodeAsync(string code)
    {
        var brand = await Q_GetByFilter(b => b.Code == code).AsNoTracking().FirstOrDefaultAsync();
        return _mapper.Map<ResponseBrandDto>(brand);
    }
    public async Task<ResponseBrandWithProductsDto?> GetBrandWithProductsById(int id)
    => _mapper.Map<ResponseBrandWithProductsDto>(
            await Q_GetWithInclude(b => b.Products).Where(b => b.Id == id).AsNoTracking().FirstOrDefaultAsync()
    );
    public async Task<int> GetBrandProductCountAsync(int id)
    => await Q_GetByFilter(b => b.Id == id).Select(b => b.Products.Count()).FirstOrDefaultAsync();

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