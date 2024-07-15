using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.DTOs;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Categories.Commands.Add;

public sealed record AddCategoryCommand(CategoryDTO Category) : ICommand<Category>;

internal sealed class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand, Category>
{
    private readonly ICategoryService _categoryService;
    private readonly IUnitOfWork _unitOfWork;

    public AddCategoryCommandHandler(ICategoryService categoryService, IUnitOfWork unitOfWork)
    {
        _categoryService = categoryService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Category>> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = Category.FromDTO(command.Category);
        var result = await _categoryService.AddAsync(category);

        if (result.IsFailed)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(category);
    }
}