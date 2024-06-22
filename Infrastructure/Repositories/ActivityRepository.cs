using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public interface IActivityRepository : IRepository<Activity>
{
    Task<IList<Activity>> ListAsync(ActivityCriteria criteria);
    Task<IPagedList<Activity>> ListAsync(ActivityCriteria criteria, int pageSize, int pageNumber);
}

public class ActivityRepository : Repository<Activity>, IActivityRepository
{
    public ActivityRepository(ApplicationContext dbContext) : base(dbContext) { }

    public async Task<IList<Activity>> ListAsync(ActivityCriteria criteria)
    {
        return await BuildQuery(criteria).ToListAsync();
    }

    public async Task<IPagedList<Activity>> ListAsync(ActivityCriteria criteria, int pageSize, int pageNumber)
    {
        return await BuildQuery(criteria).ToListAsync(pageSize, pageNumber);
    }

    protected IQueryable<Activity> BuildQuery(ActivityCriteria criteria)
    {
        var query = DbContext.Set<Activity>().AsQueryable();

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

public class ActivityCriteria
{
    public Guid? CustomerId { get; set; }
}