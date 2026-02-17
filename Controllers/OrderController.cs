
using ECommerceAPI.Entities.Enums;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _orderService.GetAllWithDto();

        return (res is null || res.Count == 0) 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("byId/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await _orderService.GetByIdWithDto(id);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("byCode/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var res = await _orderService.GetOrderByCodeAsync(code);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("byUserId/{userId:int}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var res = await _orderService.GetOrderByUserIdAsync(userId);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpGet("withProductById/{id:int}")]
    public async Task<IActionResult> GetOrderWithProductById(int id)
    {
        var res = await _orderService.GetOrderWithProductByIdAsync(id);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpPatch("activate/{id:int}")]
    public async Task<IActionResult> Activate(int id)
    {
        var res = await _orderService.ActivateOrderByIdAsync(id);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var res = await _orderService.DeactivateOrderByIdAsync(id);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }

    [HttpPatch("changeStatus/{id:int}")]
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] OrderStatus status)
    {
        var res = await _orderService.ChangeOrderStatusByIdAsync(id, status);

        return res is null 
                ? NotFound() 
                : Ok(res);
    }
}