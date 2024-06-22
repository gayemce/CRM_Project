using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public interface IContactRepository : IRepository<Contact>
{
    Task<IList<Contact>> ListAsync(ContactCriteria criteria);
    Task<IPagedList<Contact>> ListAsync(ContactCriteria criteria, int pageSize, int pageNumber);
}

public class ContactRepository : Repository<Contact>, IContactRepository
{
    public ContactRepository(ApplicationContext dbContext) : base(dbContext) { }

    public async Task<IList<Contact>> ListAsync(ContactCriteria criteria)
    {
        return await BuildQuery(criteria).ToListAsync();
    }

    public async Task<IPagedList<Contact>> ListAsync(ContactCriteria criteria, int pageSize, int pageNumber)
    {
        return await BuildQuery(criteria).ToListAsync(pageSize, pageNumber);
    }

    protected IQueryable<Contact> BuildQuery(ContactCriteria criteria)
    {
        var query = DbContext.Set<Contact>().AsQueryable();

        if (criteria.CustomerId != null)
        {
            query = query.Where(e => e.Customer.Id == criteria.CustomerId);
        }

        if (criteria.Email != null)
        {
            query = query.Where(e => e.Email.Contains(criteria.Email));
        }

        query = query
            .Include(p => p.Customer)
            .OrderBy(p => p.FirstName);

        return query;
    }
}

public class ContactCriteria
{
    public Guid? CustomerId { get; set; }
    public string? Email { get; set; }
}