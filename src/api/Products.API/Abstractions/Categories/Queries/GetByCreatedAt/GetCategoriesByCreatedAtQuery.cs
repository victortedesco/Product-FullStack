using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Categories.Queries.GetByCreatedAt;

public sealed record GetCategoriesByCreatedAtQuery(DateOnly Date) : IQuery<IEnumerable<Category>>;

internal sealed class GetCategoriesByCreatedAtQueryHandler : IQueryHandler<GetCategoriesByCreatedAtQuery, IEnumerable<Category>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoriesByCreatedAtQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<Result<IEnumerable<Category>>> Handle(GetCategoriesByCreatedAtQuery query, CancellationToken cancellationToken)
    {
        return Result.Ok(await _categoryService.GetByCreatedAtAsync(query.Date));
    }
}
