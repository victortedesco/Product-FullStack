using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Products.Queries.GetAll;

public sealed record GetAllProductsQuery() : IQuery<IEnumerable<Product>>;

internal sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IProductService _productService;

    public GetAllProductsQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Result<IEnumerable<Product>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        return Result.Ok(await _productService.GetAllAsync());
    }
}
