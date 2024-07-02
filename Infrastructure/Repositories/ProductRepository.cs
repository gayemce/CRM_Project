using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public interface IProductRepository : IRepository<Product>
{
    Task<IList<Product>> ListAsync(ProductCriteria criteria);
    Task<IPagedList<Product>> ListAsync(ProductCriteria criteria, int pageSize, int pageNumber);
}

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationContext dbContext) : base(dbContext) { }

    public async Task<IList<Product>> ListAsync(ProductCriteria criteria)
    {
        return await BuildQuery(criteria).ToListAsync();
    }

    public async Task<IPagedList<Product>> ListAsync(ProductCriteria criteria, int pageSize, int pageNumber)
    {
        return await BuildQuery(criteria).ToListAsync(pageSize, pageNumber);
    }

    protected IQueryable<Product> BuildQuery(ProductCriteria criteria)
    {
        var query = DbContext.Set<Product>().AsQueryable();

        if (criteria.Name != null)
        {
            query = query.Where(e => e.Name.Contains(criteria.Name));
        }

        query = query
            .OrderBy(p => p.Name);

        return query;
    }
}

public class ProductCriteria
{
    public string? Name { get; set; }
}

