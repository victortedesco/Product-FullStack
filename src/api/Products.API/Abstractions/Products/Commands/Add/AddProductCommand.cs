using FluentResults;
using Products.API.Abstractions.Messaging;
using Products.Domain.DTOs;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Services;

namespace Products.API.Abstractions.Products.Commands.Add;

public sealed record AddProductCommand(ProductDTO Product) : ICommand<Product>;

internal sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand, Product>
{
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductCommandHandler(IProductService productService, IUnitOfWork unitOfWork)
    {
        _productService = productService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Product>> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product.FromDTO(command.Product);
        var result = await _productService.AddAsync(product);

        if (result.IsFailed)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(product);
    }
}