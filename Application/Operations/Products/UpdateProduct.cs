using FluentValidation;
using Infrastructure;
using MediatR;

namespace Application;
public sealed record UpdateProductCommand(
    Guid ProductId,
    string Name,
    decimal Price,
    int Quantity,
    string Description) : IRequest;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Price).NotEmpty().GreaterThan(0m);
        RuleFor(c => c.Quantity).NotEmpty().GreaterThan(0);
        RuleFor(c => c.Description).NotEmpty();
    }
}

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IValidator<UpdateProductCommand> _validator;
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(
        IValidator<UpdateProductCommand> validator,
        IProductRepository productRepository)
    {
        _validator = validator;
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var product = await _productRepository.LoadAsync(command.ProductId);

        product.Name = command.Name;
        product.Price = command.Price;
        product.Quantity = command.Quantity;
        product.Description = command.Description;
        product.UpdateDate = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product);
    }
}
