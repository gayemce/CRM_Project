using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public interface IOpportunityRepository : IRepository<Opportunity>
{
    Task<IList<Opportunity>> ListAsync(OpportunityCriteria criteria);
    Task<IPagedList<Opportunity>> ListAsync(OpportunityCriteria criteria, int pageSize, int pageNumber);
}

public class OpportunityRepository : Repository<Opportunity>, IOpportunityRepository
{
    public OpportunityRepository(ApplicationContext dbContext) : base(dbContext) { }

    public async Task<IList<Opportunity>> ListAsync(OpportunityCriteria criteria)
    {
        return await BuildQuery(criteria).ToListAsync();
    }

    public async Task<IPagedList<Opportunity>> ListAsync(OpportunityCriteria criteria, int pageSize, int pageNumber)
    {
        return await BuildQuery(criteria).ToListAsync(pageSize, pageNumber);
    }

    protected IQueryable<Opportunity> BuildQuery(OpportunityCriteria criteria)
    {
        var query = DbContext.Set<Opportunity>().AsQueryable();

        if (criteria.CustomerId != null)
        {
            query = query.Where(e => e.Customer.Id == criteria.CustomerId);
        }

        query = query
            .Include(p => p.Customer)
            .OrderBy(p => p.CreateDate);

        return query;
    }
}

public class OpportunityCriteria
{
    public Guid? CustomerId { get; set; }
}

