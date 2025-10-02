
using System.Linq.Expressions;
using AutoMapper;
using ECommerceAPI.Repository.RepositoryInterfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceAPI.Services.Interfaces;

public class BaseService<T, TResponseDto, TCreateDto> : IBaseService<T, TResponseDto, TCreateDto>
    where T : class
    where TResponseDto : class
    where TCreateDto : class
{
    protected readonly IEnumerableRepository<T> E_repository;
    protected readonly IQueryableRepository<T> Q_repository;
    protected readonly IMapper _mapper;
    protected readonly IValidator<TCreateDto> _validator;

    public BaseService(IEnumerableRepository<T> enumerableRepository,
                        IQueryableRepository<T> queryableRepository,
                        IMapper mapper,
                        IValidator<TCreateDto> validator)
    {
        E_repository = enumerableRepository;
        Q_repository = queryableRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public virtual async Task<IEnumerable<TResponseDto>?> E_GetAll()
    {
        var entities = await E_repository.E_GetAll();

        return
            entities != null
                ? _mapper.Map<IEnumerable<TResponseDto>>(entities)
                : null;

    }
    public virtual async Task<TResponseDto?> E_GetById(int id)
    {
        var entity = await E_repository.E_GetById(id);
        return
            entity != null
                ? _mapper.Map<TResponseDto>(entity)
                : null;

    }
    public virtual async Task<TResponseDto> E_AddEntity(TCreateDto createDto)
    {
        var validationResponse = await _validator.ValidateAsync(createDto);

        if (validationResponse.IsValid)
        {
            return null;
        }

    }
    public virtual async Task<TResponseDto?> E_DeleteEntity(int id)
    {
        var entity = await E_repository.E_GetById(id);

        if (entity != null)
        {
            await E_repository.E_DeleteEntity(id);
            return _mapper.Map<TResponseDto>(entity);
        }

        return null;

    }
    public virtual async Task<TResponseDto> E_UpdateEntity(TCreateDto createDto)
    {
        var validationResponse = await _validator.ValidateAsync(createDto);

        if (validationResponse.IsValid)
        {
            var entity = _mapper.Map<T>(createDto);
            await E_repository.E_UpdateEntity(entity);
            return _mapper.Map<TResponseDto>(entity);
        }

    }


    public virtual IQueryable<TResponseDto> Q_GetAll()
    {
        var entities = Q_repository.Q_GetAll();
        return _mapper.Map<IQueryable<TResponseDto>>(entities);
    }
    public virtual IQueryable<TResponseDto> Q_GetByFilter(Expression<Func<T, bool>>? predicate = null)
    {
        var entities = Q_repository.Q_GetByFilter(predicate);
        return _mapper.Map<IQueryable<TResponseDto>>(entities);
    }
    public virtual IQueryable<TResponseDto> Q_MultiplerFilter(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes)
    {
        var entities = Q_repository.Q_MultiplerFilter(filters, includes);
        return _mapper.Map<IQueryable<TResponseDto>>(entities);
    }
    public virtual IQueryable<T> Q_GetWithInclude(params Expression<Func<T, object>>[]? includes)
    => Q_repository.Q_GetWithInclude(includes);
    public virtual IQueryable<T> Q_GetWithIncludeAsSplitQuery(params Expression<Func<T, object>>[]? includes)
    => Q_repository.Q_GetWithIncludeAsSplitQuery(includes);


}