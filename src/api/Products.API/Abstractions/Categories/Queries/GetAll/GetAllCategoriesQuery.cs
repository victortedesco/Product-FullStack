using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Categories.Queries.GetAll;

public sealed record GetAllCategoriesQuery() : IQuery<IEnumerable<Category>>;

internal sealed class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    private readonly ICategoryService _categoryService;

    public GetAllCategoriesQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<Result<IEnumerable<Category>>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
    {
        return Result.Ok(await _categoryService.GetAllAsync());
    }
}
