
namespace OrderEase.DataAccess.Repositories;

public interface IRepository<T>
{
    Task<T> InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> SelectAsync(int id);
    IQueryable<T> SelectAllAsQueryable();
}