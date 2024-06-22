using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public interface IRepository<T> where T : class, IEntity
{
    Task<T> CreateAsync(T entity);
    ValueTask<T> LoadAsync(Guid id);
    ValueTask<T?> GetAsync(Guid? id);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IList<T>> ListAsync();
    Task<IPagedList<T>> ListAsync(int pageSize, int pageNumber);
}

public abstract class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected Repository(ApplicationContext dbContext)
    {
        DbContext = dbContext;
    }

    protected ApplicationContext DbContext { get; }

    public virtual async Task<T> CreateAsync(T entity)
    {
        await DbContext.Set<T>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async ValueTask<T?> GetAsync(Guid? id)
    {
        return await DbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<IList<T>> ListAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<IPagedList<T>> ListAsync(int pageSize, int pageNumber)
    {
        return await DbContext.Set<T>().ToListAsync(pageSize, pageNumber);
    }

    public virtual async ValueTask<T> LoadAsync(Guid id)
    {
        var entity = await GetAsync(id);

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(T), id);
        }

        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        DbContext.Set<T>().Update(entity);
        await DbContext.SaveChangesAsync();
    }
}
