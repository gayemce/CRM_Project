using FluentValidation;
using Infrastructure;
using MediatR;

namespace Application;
public sealed record UpdateCustomerCommand(
    Guid CustomerId,
    string Name,
    string Email,
    string Phone,
    string Address) : IRequest;

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.Address).NotEmpty();
    }
}

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IValidator<UpdateCustomerCommand> _validator;
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(
        IValidator<UpdateCustomerCommand> validator,
        ICustomerRepository customerRepository)
    {
        _validator = validator;
        _customerRepository = customerRepository;
    }

    public async Task Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var customer = await _customerRepository.LoadAsync(command.CustomerId);

        customer.Name = command.Name;
        customer.Email = command.Email;
        customer.Phone = command.Phone;
        customer.Address = command.Address;
        customer.UpdateDate = DateTime.UtcNow;

        await _customerRepository.UpdateAsync(customer);
    }
}
