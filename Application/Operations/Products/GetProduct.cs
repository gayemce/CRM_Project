using Domain;
using Infrastructure;
using MediatR;

namespace Application;
public sealed record GetProductQuery(Guid ProductId) : IRequest<Product?>;

public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product?>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product?> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAsync(query.ProductId);
    }
}
