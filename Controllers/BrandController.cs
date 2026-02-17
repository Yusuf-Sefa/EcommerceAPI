
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }


    //Get
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _brandService.GetAllWithDto();
        
        if(res is null || res.Count == 0)
            return NotFound();
        
        return Ok(res);

    }

    [HttpGet("byId/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await _brandService.GetByIdWithDto(id);

        return res is null 
                ? NotFound()
                : Ok(res);
    }

    [HttpGet("byName/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var res = await _brandService.GetBrandByNameAsync(name);

        return res is null
                ? NotFound()
                : Ok(res);
    }

    [HttpGet("byCode/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var res = await _brandService.GetBrandByCodeAsync(code);

        return res is null
                ? NotFound()
                : Ok(res);

    }

    [HttpGet("withProductsById/{id:int}")]
    public async Task<IActionResult> GetBrandWithProductById(int id)
    {
        var res = await _brandService.GetBrandWithProductsById(id);

        return res is null
                ? NotFound()
                : Ok(res);
    }

    [HttpGet("getProductCountById/{id:int}")]
    public async Task<IActionResult> GetProductCountById(int id)
    {
        var res = await _brandService.GetBrandProductCountAsync(id);

        return res is null
                ? NotFound()
                : Ok(new {productCount = res});
    }


    //Patch
    [HttpPatch("activate/{id:int}")]
    public async Task<IActionResult> Activate(int id)
    {
        var res = await _brandService.ActivateBrandAsync(id);
    
        return res is null
                ? NotFound()
                : Ok(res);

    }

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var res = await _brandService.DeactivateBrandAsync(id);

        return res is null
                ? NotFound()
                : Ok(res);
    }


}