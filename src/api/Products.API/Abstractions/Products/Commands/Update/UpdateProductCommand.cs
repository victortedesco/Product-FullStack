using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.DTOs;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Products.Commands.Update;

public sealed record UpdateProductCommand(Guid Id, ProductDTO Product) : ICommand;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductService productService, IUnitOfWork unitOfWork)
    {
        _productService = productService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product.FromDTO(command.Product);
        var result = await _productService.UpdateAsync(command.Id, product);

        if (result.IsSuccess)
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}