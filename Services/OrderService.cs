
using AutoMapper;
using ECommerceAPI.Dtos.OrderDtos;
using ECommerceAPI.Entities;
using ECommerceAPI.Entities.Enums;
using ECommerceAPI.Repository.RepositoryInterfaces;
using ECommerceAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class OrderService : BaseService<Order, ResponseOrderDto, CreateOrderDto, UpdateOrderDto>, IOrderService
{
    public OrderService(IEnumerableRepository<Order> enumerableRepository,
                        IQueryableRepository<Order> queryableRepository,
                        Mapper _mapper,
                        IValidator<CreateOrderDto> _createValidator,
                        IValidator<UpdateOrderDto> _updateValidator)
    : base(enumerableRepository,
            queryableRepository,
            _mapper,
            _createValidator,
            _updateValidator)
    { }

    public async Task<ResponseOrderDto?> GetOrderByCodeAsync(string code)
    {
        var order = await Q_GetByFilter(o => o.Code == code).AsNoTracking().FirstOrDefaultAsync();
        return _mapper.Map<ResponseOrderDto>(order);
    }
    public async Task<ResponseOrderDto?> GetOrderByUserIdAsync(int userId)
    {
        var order = await Q_GetByFilter(c => c.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
        return _mapper.Map<ResponseOrderDto>(order);
    }
    public async Task<ResponseOrderDto?> GetOrderWithUserByIdAsync(int id)
    {
        var order = await Q_GetByFilter(o => o.Id == id)
                            .Include(o => o.User)
                            .AsNoTrackingWithIdentityResolution()
                            .FirstOrDefaultAsync();

        return _mapper.Map<ResponseOrderDto>(order);                    
    }
    public async Task<ResponseOrderDto?> GetOrderWithProductByIdAsync(int id)
    {
        var order = await Q_GetByFilter(o => o.Id == id)
                            .Include(o => o.ProductOrders)
                            .ThenInclude(po => po.Product)
                            .AsNoTrackingWithIdentityResolution()
                            .FirstOrDefaultAsync();

        return _mapper.Map<ResponseOrderDto>(order);
    }


    public async Task<ResponseOrderDto?> ActivateOrderByIdAsync(int id)
    {
        var order = await Q_GetByFilter(o => o.Id == id).FirstOrDefaultAsync();

        if (order == null)
            return null;

        order.IsActive = true;
        await E_repository.E_UpdateEntity(order);
        return _mapper.Map<ResponseOrderDto>(order);
    }
    public async Task<ResponseOrderDto?> DeactivateOrderByIdAsync(int id)
    {
        var order = await Q_GetByFilter(o => o.Id == id).FirstOrDefaultAsync();

        if (order == null)
            return null;

        order.IsActive = false;
        await E_repository.E_UpdateEntity(order);
        return _mapper.Map<ResponseOrderDto>(order);
    }
    public async Task<ResponseOrderDto?> ChangeOrderStatusByIdAsync(int id, OrderStatus status)
    {
        var order = await Q_GetByFilter(o => o.Id == id).FirstOrDefaultAsync();

        if (order == null)
            return null;

        order.Status = status;
        await E_repository.E_UpdateEntity(order);
        return _mapper.Map<ResponseOrderDto>(order);
    }


}