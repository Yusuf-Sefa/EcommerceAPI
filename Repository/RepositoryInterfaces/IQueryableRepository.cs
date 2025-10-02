
using System.Linq.Expressions;

namespace ECommerceAPI.Repository.RepositoryInterfaces;

public interface IQueryableRepository<T> where T : class
{
    IQueryable<T> Q_GetAll();
    IQueryable<T> Q_GetByFilter(Expression<Func<T, bool>>? predicate = null);
    IQueryable<T> Q_MultiplerFilter(Expression<Func<T, bool>>[] filters, params Expression<Func<T, object>>[] includes);
    IQueryable<T> Q_GetWithInclude(params Expression<Func<T, object>>[]? includes);
    IQueryable<T> Q_GetWithIncludeAsSplitQuery(params Expression<Func<T, object>>[]? includes);
    
}
