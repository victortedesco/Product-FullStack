using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Products.Queries.GetByUpdatedAt;

public sealed record GetProductByUpdatedAtQuery(DateOnly Date) : IQuery<IEnumerable<Product>>;

internal sealed class GetProductByUpdatedAtQueryHandler : IQueryHandler<GetProductByUpdatedAtQuery, IEnumerable<Product>>
{
    private readonly IProductService _productService;

    public GetProductByUpdatedAtQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Result<IEnumerable<Product>>> Handle(GetProductByUpdatedAtQuery query, CancellationToken cancellationToken)
    {
        return Result.Ok(await _productService.GetByUpdatedAtAsync(query.Date));
    }
}
