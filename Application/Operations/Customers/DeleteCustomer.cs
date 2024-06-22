using Infrastructure;
using MediatR;

namespace Application;
public sealed record DeleteCustomerCommand(Guid CustomerId) : IRequest;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.LoadAsync(command.CustomerId);
        await _customerRepository.DeleteAsync(customer);
    }
}