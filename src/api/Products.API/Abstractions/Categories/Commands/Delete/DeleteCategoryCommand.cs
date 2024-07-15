using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Categories.Commands.Delete;

public sealed record DeleteCategoryCommand(Guid Id) : ICommand;

internal sealed class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryService _categoryService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(ICategoryService categoryService, IUnitOfWork unitOfWork)
    {
        _categoryService = categoryService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _categoryService.DeleteAsync(command.Id);

        if (result.IsSuccess)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}