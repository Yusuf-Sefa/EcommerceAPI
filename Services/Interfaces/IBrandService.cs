
using ECommerceAPI.Dtos.BrandDtos;

namespace ECommerceAPI.Services.Interfaces;

public interface IBrandService
{

    public Task<ResponseBrandDto?> GetBrandByNameAsync(string name);
    public Task<ResponseBrandDto?> GetBrandByCodeAsync(string code);

    public ResponseBrandWithProductsDto? GetBrandWithProductsById(int id);
    public int GetBrandProductCountAsync(int id);

    public Task<ResponseBrandDto?> ActivateBrandAsync(int id);
    public Task<ResponseBrandDto?> DeactivateBrandAsync(int id);

}