using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Context;
using OrderEase.Domain.Entities;

namespace OrderEase.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext context;
    public Repository(AppDbContext context)
    {
        this.context = context;
        context.Set<T>();
    }

    public async Task<T> InsertAsync(T entity)
    {
        entity.CreatedAt = DateTime.Now;
        var create = (await context.AddAsync(entity)).Entity;
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.Now;
        context.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        entity.DeletedAt = DateTime.Now;
        entity.IsDeleted = true;
        context.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task<T> SelectAsync(int id)
    {
        return await context.Set<T>()
            .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    }

    public IQueryable<T> SelectAllAsQueryable()
    {
        return context.Set<T>().AsQueryable();
    }
}
