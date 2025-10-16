
using ECommerceAPI.Dtos.OrderDtos;
using ECommerceAPI.Entities.Enums;

namespace ECommerceAPI.Services.Interfaces;

public interface IOrderService
{
    public Task<ResponseOrderDto?> GetOrderByCodeAsync(string code);
    public Task<ResponseOrderDto?> GetOrderByUserIdAsync(int userId);
    public Task<ResponseOrderDto?> GetOrderWithUserByIdAsync(int id);
    public Task<ResponseOrderDto?> GetOrderWithProductByIdAsync(int id);

    public Task<ResponseOrderDto?> ActivateOrderByIdAsync(int id);
    public Task<ResponseOrderDto?> DeactivateOrderByIdAsync(int id);
    public Task<ResponseOrderDto?> ChangeOrderStatusByIdAsync(int id, OrderStatus status);

}