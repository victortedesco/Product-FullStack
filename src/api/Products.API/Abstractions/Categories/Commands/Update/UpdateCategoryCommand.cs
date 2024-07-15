using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.DTOs;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Categories.Commands.Update;

public sealed record UpdateCategoryCommand(Guid Id, CategoryDTO Category) : ICommand;

internal sealed class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
{
    private readonly ICategoryService _categoryService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(ICategoryService categoryService, IUnitOfWork unitOfWork)
    {
        _categoryService = categoryService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = Category.FromDTO(command.Category);
        var result = await _categoryService.UpdateAsync(command.Id, category);

        if (result.IsSuccess)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}