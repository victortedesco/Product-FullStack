using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Categories.Queries.GetByName;

public sealed record GetCategoryByNameQuery(string Name) : IQuery<Category>;

internal sealed class GetCategoryByNameHandler : IQueryHandler<GetCategoryByNameQuery, Category>
{
    private readonly ICategoryService _productService;

    public GetCategoryByNameHandler(ICategoryService productService)
    {
        _productService = productService;
    }

    public async Task<Result<Category>> Handle(GetCategoryByNameQuery query, CancellationToken cancellationToken)
    {
        var result = await _productService.GetByNameAsync(query.Name);

        if (result is null)
            return CategoryErrors.DoesNotExist;

        return Result.Ok(result);
    }
}
