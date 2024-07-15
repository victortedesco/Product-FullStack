using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.API.Abstractions.Categories.Commands.Add;
using Products.API.Abstractions.Categories.Commands.Delete;
using Products.API.Abstractions.Categories.Commands.Update;
using Products.API.Abstractions.Categories.Queries.GetAll;
using Products.API.Abstractions.Categories.Queries.GetByCreatedAt;
using Products.API.Abstractions.Categories.Queries.GetById;
using Products.API.Abstractions.Categories.Queries.GetByName;
using Products.API.Abstractions.Categories.Queries.GetByUpdatedAt;
using Products.API.ViewModels;
using Products.Domain.DTOs;
using Products.Domain.Entities;

namespace Products.API.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ISender _sender;

    public CategoryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<CategoryViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var command = new GetAllCategoriesQuery();

        var result = await _sender.Send(command);

        var categories = result.Value;

        if (!categories.Any())
            return NoContent();

        return Ok(categories.Select(c => CategoryViewModel.FromModel(c)));
    }

    [HttpGet("id/{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var command = new GetCategoryByIdQuery(id);
        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(CategoryViewModel.FromModel(result.Value)) : NotFound();
    }

    [HttpGet("createdAt/{date:datetime}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<CategoryViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCreatedAt(DateOnly date)
    {
        var command = new GetCategoriesByCreatedAtQuery(date);

        var result = await _sender.Send(command);
        var categories = result.Value;

        if (!categories.Any())
            return NoContent();

        return Ok(categories.Select(c => CategoryViewModel.FromModel(c)));
    }

    [HttpGet("updatedAt/{date:datetime}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<CategoryViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUpdatedAt(DateOnly date)
    {
        var command = new GetCategoriesByUpdatedAtQuery(date);

        var result = await _sender.Send(command);
        var categories = result.Value;

        if (!categories.Any())
            return NoContent();

        return Ok(categories.Select(c => CategoryViewModel.FromModel(c)));
    }

    [HttpGet("name/{name}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var command = new GetCategoryByNameQuery(name);

        var result = await _sender.Send(command);

        return result.IsSuccess ? Ok(CategoryViewModel.FromModel(result.Value)) : NotFound();
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody] CategoryDTO categoryRequest)
    {
        var command = new AddCategoryCommand(categoryRequest);

        var result = await _sender.Send(command);

        if (result.IsFailed)
            return BadRequest(result.ToString());

        var category = result.Value;

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, CategoryViewModel.FromModel(category));
    }

    [HttpPut("{id:guid}")]
    [Produces("text/plain")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CategoryDTO categoryRequest)
    {
        var command = new UpdateCategoryCommand(id, categoryRequest);
        var result = await _sender.Send(command);

        if (result.Errors.Contains(CategoryErrors.DoesNotExist))
            return NotFound();

        if (result.IsFailed)
            return BadRequest(result.ToString());

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Produces("text/plain")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteCategoryCommand(id);

        var result = await _sender.Send(command);

        if (result.Errors.Contains(CategoryErrors.DoesNotExist))
            return NotFound();

        return Ok();
    }
}
