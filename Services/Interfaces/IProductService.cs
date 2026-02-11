
using ECommerceAPI.Dtos.ProductDtos;
using ECommerceAPI.Entities;

namespace ECommerceAPI.Services.Interfaces;

public interface IProductService : IBaseService<Product, ResponseProductDto, CreateProductDto, UpdateProductDto>
{
    public Task<ResponseProductDto?> GetProductByName(string name);
    public Task<List<ResponseProductDto>?> GetProductByBrandName(string brandName);
    public  Task<List<ResponseProductDto>?> GetProductByPrice(int? minPrice = null, int? maxPrice = null);
    public  Task<List<ResponseProductDto>?> GetProductByStock(int? minStock = null, int? maxStock = null);
    public  Task<ResponseProductDto?> DeactiveProductById(int id);
    public  Task<ResponseProductDto?> ActivateProductById(int id);
    public  Task<ResponseProductDto?> ToggleProductActivationById(int id);
    public  Task<ResponseProductDto?> ToggleActivationByCategoryName(string name);
    public  Task<List<ResponseProductDto>?> GetProductByCategoryId(int id);
    public  Task<List<ResponseProductDto>?> GetProductByCategoryName(string name);
}