using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Categories.Queries.GetById;

public sealed record GetCategoryByIdQuery(Guid Id) : IQuery<Category>;

internal sealed class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, Category>
{
    private readonly ICategoryService _categoryService;

    public GetCategoryByIdQueryHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<Result<Category>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetByIdAsync(query.Id);

        if (result is null)
            return CategoryErrors.DoesNotExist;

        return Result.Ok(result);
    }
}
