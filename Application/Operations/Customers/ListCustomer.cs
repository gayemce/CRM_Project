using Domain;
using Infrastructure;
using MediatR;
using System.Runtime.InteropServices;

namespace Application;
public sealed record ListCustomerQuery(
    [Optional] string? Name,
    [Optional] int? PageSize,
    [Optional] int? PageNumber) :
    IRequest<IPagedList<Customer>>;

public sealed class ListCustomerQueryHandler : IRequestHandler<ListCustomerQuery, IPagedList<Customer>>
{
    private readonly ICustomerRepository _customerRepository;

    public ListCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<IPagedList<Customer>> Handle(ListCustomerQuery query, CancellationToken cancellationToken)
    {
        var criteria = new CustomerCriteria()
        {
            Name = query.Name,
        };

        var pageSize = query.PageSize ?? int.MaxValue;
        var pageNumber = query.PageNumber ?? 1;

        return _customerRepository.ListAsync(criteria, pageSize, pageNumber);
    }
}

