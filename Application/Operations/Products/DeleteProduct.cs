using Infrastructure;
using MediatR;

namespace Application;
public sealed record DeleteProductCommand(Guid ProductId) : IRequest;

public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.LoadAsync(command.ProductId);
        await _productRepository.DeleteAsync(product);
    }
}