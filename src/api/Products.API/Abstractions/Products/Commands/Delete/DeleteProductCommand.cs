using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Products.Commands.Delete;

public sealed record DeleteProductCommand(Guid Id) : ICommand;

internal sealed class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductService productService, IUnitOfWork unitOfWork)
    {
        _productService = productService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var result = await _productService.DeleteAsync(command.Id);

        if (result.IsSuccess)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}