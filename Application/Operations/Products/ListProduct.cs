using Domain;
using Infrastructure;
using MediatR;
using System.Runtime.InteropServices;

namespace Application;
public sealed record ListProductQuery(
    [Optional] string? Name,
    [Optional] int? PageSize,
    [Optional] int? PageNumber) :
    IRequest<IPagedList<Product>>;

public sealed class ListProductQueryHandler : IRequestHandler<ListProductQuery, IPagedList<Product>>
{
    private readonly IProductRepository _productRepository;

    public ListProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<IPagedList<Product>> Handle(ListProductQuery query, CancellationToken cancellationToken)
    {
        var criteria = new ProductCriteria()
        {
            Name = query.Name,
        };

        var pageSize = query.PageSize ?? int.MaxValue;
        var pageNumber = query.PageNumber ?? 1;

        return _productRepository.ListAsync(criteria, pageSize, pageNumber);
    }
}

