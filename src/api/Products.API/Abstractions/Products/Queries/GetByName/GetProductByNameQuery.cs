using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Products.Queries.GetByName;

public sealed record GetProductByNameQuery(string Name) : IQuery<IEnumerable<Product>>;

internal sealed class GetProductByNameHandler : IQueryHandler<GetProductByNameQuery, IEnumerable<Product>>
{
    private readonly IProductService _productService;

    public GetProductByNameHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Result<IEnumerable<Product>>> Handle(GetProductByNameQuery query, CancellationToken cancellationToken)
    {
        return Result.Ok(await _productService.GetByNameAsync(query.Name));
    }
}
