

using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _categoryService.GetAllWithDto();

        if(res is null || res.Count == 0)
            return NotFound();

        return Ok(res);
    }

    [HttpGet("byId/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await _categoryService.GetByIdWithDto(id);

        return res is null
                ? NotFound()
                : Ok(res);
    }

    [HttpGet("byName/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var res = await _categoryService.GetCategoryByNameAsync(name);

        return res is null
                ? NotFound()
                : Ok(res);
    }

    [HttpGet("byCode/{code}")]
    public async Task<IActionResult> GetBycode(string code)
    {
        var res = await _categoryService.GetCategoryByNameAsync(code);

        return res is null
                ? NotFound()
                : Ok(res);
    }

    [HttpGet("withProductById/{id:int}")]
    public async Task<IActionResult> GetCategoryWithProductById(int id)
    {
        var res = await _categoryService.GetCategoryWithProductsByIdAsync(id);

        return res is null
                ? NotFound()
                : Ok(res);
    }

    [HttpGet("getProductCountById/{id:int}")]
    public async Task<IActionResult> GetProductCountById(int id)
    {
        var res = await _categoryService.GetCategoryProductCountByIdAsync(id);

        return res is null
                ? NotFound()
                : Ok(new {productCount = res});
    }

    [HttpPatch("activate/{id:int}")]
    public async Task<IActionResult> Activate(int id)
    {
        var res = await _categoryService.ActivateCategoryByIdAsync(id);

        return res is null
                ? NotFound()
                : Ok(res);
    }

    [HttpPatch("dactivate/{id:int}")]
    public async Task<IActionResult> Dactivate(int id)
    {
        var res = await _categoryService.DeactivateCategoryByIdAsync(id);

        return res is null
                ? NotFound()
                : Ok(res);
    }

}