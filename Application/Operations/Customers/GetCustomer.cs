using Domain;
using Infrastructure;
using MediatR;

namespace Application;
public sealed record GetCustomerQuery(Guid CustomerId) : IRequest<Customer?>;

public sealed class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer?>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer?> Handle(GetCustomerQuery query, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetAsync(query.CustomerId);
    }
}
