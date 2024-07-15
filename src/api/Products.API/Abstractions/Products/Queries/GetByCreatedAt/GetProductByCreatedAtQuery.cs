using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Products.Queries.GetByCreatedAt;

public sealed record GetProductByCreatedAtQuery(DateOnly Date) : IQuery<IEnumerable<Product>>;

internal sealed class GetProductByCreatedAtQueryHandler : IQueryHandler<GetProductByCreatedAtQuery, IEnumerable<Product>>
{
    private readonly IProductService _productService;

    public GetProductByCreatedAtQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Result<IEnumerable<Product>>> Handle(GetProductByCreatedAtQuery query, CancellationToken cancellationToken)
    {
        return Result.Ok(await _productService.GetByCreatedAtAsync(query.Date));
    }
}
