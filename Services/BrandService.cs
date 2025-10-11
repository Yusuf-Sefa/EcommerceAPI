
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

public class BrandService : BaseService<Brand, ResponseBrandDto, CreateBrandDto, UpdateBrandActivityDto>, IBrandService
{

    public BrandService(IEnumerableRepository<Brand> enumerableRepository,
                        IQueryableRepository<Brand> queryableRepository,
                        IMapper _mapper,
                        IValidator<CreateBrandDto> _validator)
    : base(enumerableRepository,
            queryableRepository,
            _mapper,
            _validator)
    { }

    public async Task<ResponseBrandDto?> GetBrandByNameAsync(string name)
    {
        var brand = await Q_GetByFilter(b => b.Name == name).FirstOrDefaultAsync();

        return _mapper.Map<ResponseBrandDto>(brand);
    }
    public async Task<ResponseBrandDto?> GetBrandByCodeAsync(string code)
    {
        var brand = await Q_GetByFilter(b => b.Code == code).FirstOrDefaultAsync();

        return _mapper.Map<ResponseBrandDto>(brand);
    }

    public async Task<ResponseBrandDto?> ActivateBrandAsync(int id)
    {
        var brand = await E_GetById(id);

        if (brand == null)
            return null;

        brand.IsActive = true;
        var updateDto = _mapper.Map<UpdateBrandActivityDto>(brand);
        return await E_UpdateEntity(updateDto);

    }
    public async Task<ResponseBrandDto?> DeactivateBrandAsync(int id)
    {
        var brand = await E_GetById(id);

        if (brand == null)
            return null;

        brand.IsActive = false;
        var updateDto = _mapper.Map<UpdateBrandActivityDto>(brand);
        return await E_UpdateEntity(updateDto);

    }

    public IQueryable<ResponseBrandWithProductsDto> GetBrandWithProductsById(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<int> GetBrandProductCountAsync(int id)
    => await Q_GetByFilter(b => b.Id == id).Select(b => b.Products.Count()).FirstOrDefaultAsync();

}