using Domain;
using FluentValidation;
using Infrastructure;
using MediatR;

namespace Application;
public sealed record CreateProductCommand(
    string Name,
    decimal Price,
    int Quantity,
    string Description) :
    IRequest<Product>;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Price).NotEmpty().GreaterThan(0m);
        RuleFor(c => c.Quantity).NotEmpty().GreaterThan(0);
        RuleFor(c => c.Description).NotEmpty();
    }
}

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IValidator<CreateProductCommand> _validator;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(
        IValidator<CreateProductCommand> validator,
        IProductRepository productRepository)
    {
        _validator = validator;
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var Product = new Product(command.Name, command.Price, command.Quantity, command.Description);

        await _productRepository.CreateAsync(Product);
        Product = await _productRepository.LoadAsync(Product.Id);

        return Product;
    }
}

