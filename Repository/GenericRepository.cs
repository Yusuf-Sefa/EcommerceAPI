
using System.Linq.Expressions;
using ECommerceAPI.APIContext;
using ECommerceAPI.Entities.Interfaces;
using ECommerceAPI.Repository.RepositoryInterfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repository;

public class GenericRepository<T> : IEnumerableRepository<T>, IQueryableRepository<T> where T : class, IEntityIdBase
{

    private readonly Context _dbContext;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(Context context)
    {
        _dbContext = context;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }


    //Enumerable methods//
    public async Task<IEnumerable<T>> E_GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }
    public async Task<T?> E_GetById(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task E_AddEntity(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }
    public async Task E_DeleteEntity(int id)
    {
        var entity = await E_GetById(id);
        _dbSet.Remove(entity);
        await SaveChangesAsync();
    }
    public async Task E_UpdateEntity(T entity)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync();
    }


    //Queryable methods//
    public IQueryable<T> Q_GetAll()
    {
        return _dbSet.AsNoTracking();
    }
    public IQueryable<T> Q_GetByFilter(Expression<Func<T, bool>>? predicate = null)
    {
        return _dbSet.Where(predicate).AsNoTracking();
    }
    public IQueryable<T> Q_GetWithInclude(params Expression<Func<T, object>>[]? includes)
    {
        var dbSet = _dbSet.AsNoTracking();

        foreach (var expression in includes)
        {
            dbSet = dbSet.Include(expression);
        }

        return dbSet;
    }
    public IQueryable<T> Q_GetWithIncludeAsSplitQuery(params Expression<Func<T, object>>[]? includes)
    {
        var dbSet = _dbSet.AsSplitQuery().AsNoTracking();

        foreach (var expression in includes)
        {
            dbSet = dbSet.Include(expression);
        }

        return dbSet;
    }
    public IQueryable<T> Q_MultiplerFilter(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes)
    {
        var dbSet = _dbSet.AsNoTracking();

        if (includes != null && includes.Length != 0)
            foreach (var include in includes) dbSet = dbSet.Include(include);

        if (filters != null && filters.Length != 0)
            foreach (var filter in filters) dbSet = dbSet.Where(filter);

        return dbSet;

    }
    
}