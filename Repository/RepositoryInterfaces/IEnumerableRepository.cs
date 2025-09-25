
namespace ECommerceAPI.Repository.RepositoryInterfaces;

public interface IEnumerableRepository<T> where T : class
{
    Task<IEnumerable<T>> E_GetAll();
    Task<T> E_GetById(int id);
    Task E_AddEntity(T entity);
    Task E_UpdateEntity(T entity);
    Task E_DeleteEntity(int id);

}