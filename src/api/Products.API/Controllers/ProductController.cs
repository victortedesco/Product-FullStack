using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.API.Abstractions.Products.Commands.Add;
using Products.API.Abstractions.Products.Commands.Delete;
using Products.API.Abstractions.Products.Commands.Update;
using Products.API.Abstractions.Products.Queries.GetAll;
using Products.API.Abstractions.Products.Queries.GetByCreatedAt;
using Products.API.Abstractions.Products.Queries.GetById;
using Products.API.Abstractions.Products.Queries.GetByName;
using Products.API.Abstractions.Products.Queries.GetByUpdatedAt;
using Products.API.ViewModels;
using Products.Domain.DTOs;
using Products.Domain.Entities;

namespace Products.API.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), StatusCodes.Status200OK, "application/json")]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetAllProductsQuery();
        var result = await _sender.Send(command);

        var products = result.Value;

        if (!products.Any())
            return NoContent();

        return Ok(products.Select(p => ProductViewModel.FromModel(p)));
    }

    [HttpGet("id/{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var command = new GetProductByIdQuery(id);

        var result = await _sender.Send(command);
        var product = result.Value;

        if (product is null)
            return NotFound();

        return Ok(ProductViewModel.FromModel(product));
    }

    [HttpGet("createdAt/{date:datetime}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCreatedAt(DateOnly date)
    {
        var command = new GetProductByCreatedAtQuery(date);
        var result = await _sender.Send(command);

        var products = result.Value;

        if (!products.Any())
            return NoContent();

        return Ok(products.Select(p => ProductViewModel.FromModel(p)));
    }

    [HttpGet("updatedAt/{date:datetime}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUpdatedAt(DateOnly date)
    {
        var command = new GetProductByUpdatedAtQuery(date);
        var result = await _sender.Send(command);

        var products = result.Value;

        if (!products.Any())
            return NoContent();

        return Ok(products.Select(p => ProductViewModel.FromModel(p)));
    }

    [HttpGet("name/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), StatusCodes.Status200OK, "application/json")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var command = new GetProductByNameQuery(name);
        var result = await _sender.Send(command);

        var products = result.Value;

        if (!products.Any())
            return NoContent();

        return Ok(products.Select(p => ProductViewModel.FromModel(p)));
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, "text/plain")]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status201Created, "application/json")]
    public async Task<IActionResult> Add([FromBody] ProductDTO productRequest)
    {
        var command = new AddProductCommand(productRequest);
        var result = await _sender.Send(command);

        if (result.IsFailed)
            return BadRequest(result.ToString());

        var product = result.Value;

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductViewModel.FromModel(product));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductDTO productRequest)
    {
        var command = new UpdateProductCommand(id, productRequest);
        var result = await _sender.Send(command);

        if (result.Errors.Contains(CategoryErrors.DoesNotExist))
            return NotFound();

        if (result.IsFailed)
            return BadRequest(result.ToString());

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteProductCommand(id);

        var result = await _sender.Send(command);

        if (result.Errors.Contains(ProductErrors.DoesNotExist))
            return NotFound();

        return Ok();
    }
}
