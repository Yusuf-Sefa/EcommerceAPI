
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerceAPI.Repository.RepositoryInterfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services.Interfaces;

public class BaseService<T, TResponseDto, TCreateDto,TUpdateDto> : IBaseService<T, TResponseDto, TCreateDto, TUpdateDto>
    where T : class
    where TResponseDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    protected readonly IEnumerableRepository<T> E_repository;
    protected readonly IQueryableRepository<T> Q_repository;
    protected readonly IMapper _mapper;
    protected readonly IValidator<TCreateDto> _createValidator;
    protected readonly IValidator<TUpdateDto> _updateValidator;

    public BaseService(IEnumerableRepository<T> enumerableRepository,
                        IQueryableRepository<T> queryableRepository,
                        IMapper mapper,
                        IValidator<TCreateDto> createValidator,
                        IValidator<TUpdateDto> updateValidator)
    {
        E_repository = enumerableRepository;
        Q_repository = queryableRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public virtual async Task<IEnumerable<TResponseDto>?> E_GetAll()
    {
        var entities = await E_repository.E_GetAll();

        return _mapper.Map<IEnumerable<TResponseDto>>(entities);
    }
    public virtual async Task<TResponseDto?> E_GetById(int id)
    {
        var entity = await E_repository.E_GetById(id);

        return _mapper.Map<TResponseDto>(entity);
    }
    public virtual async Task<TResponseDto?> E_AddEntity(TCreateDto createDto)
    {
        var validationResponse = await _createValidator.ValidateAsync(createDto);

        if (!validationResponse.IsValid)
            throw new ValidationException(validationResponse.Errors.ToString());

        var entity = _mapper.Map<T>(createDto);
        await E_repository.E_AddEntity(entity);

        return _mapper.Map<TResponseDto>(entity);
    }
    public virtual async Task<TResponseDto?> E_DeleteEntity(int id)
    {
        var entity = await E_repository.E_GetById(id);

        await E_repository.E_DeleteEntity(id);

        return _mapper.Map<TResponseDto>(entity);
    }
    public virtual async Task<TResponseDto?> E_UpdateEntity(TUpdateDto updateDto)
    {
        var validationResponse = await _updateValidator.ValidateAsync(updateDto);

        if (!validationResponse.IsValid)
            throw new ValidationException(validationResponse.Errors.ToString());

        var entity = _mapper.Map<T>(updateDto);
        await E_repository.E_UpdateEntity(entity);

        return _mapper.Map<TResponseDto>(entity);
    }


    public virtual IQueryable<T> Q_GetAll() 
    => Q_repository.Q_GetAll();
    public virtual IQueryable<T> Q_GetByFilter(Expression<Func<T, bool>>? predicate = null)
    => Q_repository.Q_GetByFilter(predicate);
    public virtual IQueryable<T> Q_MultiplerFilter(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes)
    => Q_repository.Q_MultiplerFilter(filters, includes);
    public virtual IQueryable<T> Q_GetWithInclude(params Expression<Func<T, object>>[]? includes)
    => Q_repository.Q_GetWithInclude(includes);
    public virtual IQueryable<T> Q_GetWithIncludeAsSplitQuery(params Expression<Func<T, object>>[]? includes)
    => Q_repository.Q_GetWithIncludeAsSplitQuery(includes);


    public virtual async Task<List<TResponseDto>?> GetAllWithDto()
    {
        var entity = Q_repository.Q_GetAll();

        return await entity
                        .ProjectTo<TResponseDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();        
    }
    public virtual async Task<TResponseDto?> GetByIdWithDto(int id)
    {
        var entity = Q_repository.Q_GetAll().Where(e => EF.Property<int>(e, "Id") == id);

        return await entity
                        .ProjectTo<TResponseDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
    }

}