using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Categories.Queries.GetByUpdatedAt;

public sealed record GetCategoriesByUpdatedAtQuery(DateOnly Date) : IQuery<IEnumerable<Category>>;

internal sealed class GetCategoriesByUpdatedAtQueryHandler : IQueryHandler<GetCategoriesByUpdatedAtQuery, IEnumerable<Category>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoriesByUpdatedAtQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<Result<IEnumerable<Category>>> Handle(GetCategoriesByUpdatedAtQuery query, CancellationToken cancellationToken)
    {
        return Result.Ok(await _categoryService.GetByUpdatedAtAsync(query.Date));
    }
}
