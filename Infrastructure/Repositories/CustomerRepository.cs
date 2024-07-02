using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public interface ICustomerRepository : IRepository<Customer>
{
    Task<IList<Customer>> ListAsync(CustomerCriteria criteria);
    Task<IPagedList<Customer>> ListAsync(CustomerCriteria criteria, int pageSize, int pageNumber);
}

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationContext dbContext) : base(dbContext) { }

    public async Task<IList<Customer>> ListAsync(CustomerCriteria criteria)
    {
        return await BuildQuery(criteria).ToListAsync();
    }

    public async Task<IPagedList<Customer>> ListAsync(CustomerCriteria criteria, int pageSize, int pageNumber)
    {
        return await BuildQuery(criteria).ToListAsync(pageSize, pageNumber);
    }

    protected IQueryable<Customer> BuildQuery(CustomerCriteria criteria)
    {
        var query = DbContext.Set<Customer>().AsQueryable();

        if (criteria.Name != null)
        {
            query = query.Where(e => e.Name.Contains(criteria.Name));
        }

        query = query
            // .Include(p => p.Contacts)
            .OrderBy(p => p.Name);

        return query;
    }
}

public class CustomerCriteria
{
    public string? Name { get; set; }
}

