
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _productService.GetAllWithDto();

        return (res is null || res.Count == 0) 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("byId/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await _productService.GetByIdWithDto(id);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("byName/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var res = await _productService.GetProductByName(name);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("byBrand/{brandName}")]
    public async Task<IActionResult> GetByBrandName(string brandName)
    {
        var res = await _productService.GetProductByBrandName(brandName);

        return (res is null || res.Count == 0) 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("filterByPrice")]
    public async Task<IActionResult> GetByPrice([FromQuery] int? minPrice, [FromQuery] int? maxPrice)
    {
        var res = await _productService.GetProductByPrice(minPrice, maxPrice);

        return (res is null || res.Count == 0) 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("filterByStock")]
    public async Task<IActionResult> GetByStock([FromQuery] int? minStock, [FromQuery] int? maxStock)
    {
        var res = await _productService.GetProductByStock(minStock, maxStock);

        return (res is null || res.Count == 0) 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("byCategoryId/{id:int}")]
    public async Task<IActionResult> GetByCategoryId(int id)
    {
        var res = await _productService.GetProductByCategoryId(id);

        return (res is null || res.Count == 0) 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("byCategoryName/{name}")]
    public async Task<IActionResult> GetByCategoryName(string name)
    {
        var res = await _productService.GetProductByCategoryName(name);

        return (res is null || res.Count == 0) 
                ? NotFound() 
                : Ok(res);
    }

    [HttpPatch("activate/{id:int}")]
    public async Task<IActionResult> Activate(int id)
    {
        var res = await _productService.ActivateProductById(id);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var res = await _productService.DeactiveProductById(id);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpPatch("toggleActivation/{id:int}")]
    public async Task<IActionResult> ToggleActivation(int id)
    {
        var res = await _productService.ToggleProductActivationById(id);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpPatch("toggleByCategory/{name}")]
    public async Task<IActionResult> ToggleByCategoryName(string name)
    {
        var res = await _productService.ToggleActivationByCategoryName(name);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }
}