
namespace OrderEase.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    public Task DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> InsertAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> SelectAllAsQueryable()
    {
        throw new NotImplementedException();
    }

    public Task<T> SelectAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
