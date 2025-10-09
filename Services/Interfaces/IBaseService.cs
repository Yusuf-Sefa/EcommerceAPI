
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Services.Interfaces;

public interface IBaseService<T, TResponseDto, TCreateDto, TUpdateDto> 
    where T : class
    where TResponseDto : class
    where TCreateDto : class 
{
    //Enumerables//
    Task<IEnumerable<TResponseDto>?> E_GetAll();
    Task<TResponseDto?> E_GetById(int id);
    Task<TResponseDto?> E_AddEntity(TCreateDto createDto);
    Task<TResponseDto?> E_DeleteEntity(int id);
    Task<TResponseDto?> E_UpdateEntity(TUpdateDto updateDto);


    //Queryables//
    IQueryable<TResponseDto> Q_GetAll();
    IQueryable<TResponseDto> Q_GetByFilter(Expression<Func<T, bool>>? predicate = null);
    IQueryable<T> Q_GetWithInclude(params Expression<Func<T, object>>[]? includes);
    IQueryable<T> Q_GetWithIncludeAsSplitQuery(params Expression<Func<T, object>>[]? includes);
    IQueryable<TResponseDto> Q_MultiplerFilter(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes);

}