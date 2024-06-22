using Domain;
using FluentValidation;
using Infrastructure;
using MediatR;

namespace Application;
public sealed record CreateCustomerCommand(
    string Name,
    string Email,
    string Phone,
    string Address) :
    IRequest<Customer>;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.Phone).NotEmpty();
        RuleFor(c => c.Address).NotEmpty();
    }
}

public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
{
    private readonly IValidator<CreateCustomerCommand> _validator;
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(
        IValidator<CreateCustomerCommand> validator,
        ICustomerRepository customerRepository)
    {
        _validator = validator;
        _customerRepository = customerRepository;
    }

    public async Task<Customer> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var Customer = new Customer(command.Name, command.Email, command.Phone, command.Address);

        await _customerRepository.CreateAsync(Customer);
        Customer = await _customerRepository.LoadAsync(Customer.Id);

        return Customer;
    }
}

