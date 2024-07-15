using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Products.Queries.GetById;

public sealed record GetProductByIdQuery(Guid Id) : IQuery<Product?>;

internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductService _productService;

    public GetProductByIdQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<Result<Product?>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        return Result.Ok(await _productService.GetByIdAsync(query.Id));
    }
}
