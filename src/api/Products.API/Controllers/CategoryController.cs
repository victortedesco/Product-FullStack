using Microsoft.AspNetCore.Mvc;
using Products.API.ViewModels;
using Products.Domain.DTOs;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Repository;

namespace Products.API.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = categoryRepository.UnitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<CategoryViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryRepository.GetAllAsync();

        if (!categories.Any())
            return NoContent();

        return Ok(categories.Select(c => CategoryViewModel.FromModel(c)));
    }

    [HttpGet("id/{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return NotFound();

        return Ok(CategoryViewModel.FromModel(category));
    }

    [HttpGet("name/{name}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var category = await _categoryRepository.GetByNameAsync(name);

        if (category is null)
            return NotFound();

        return Ok(CategoryViewModel.FromModel(category));
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody] CategoryDTO categoryRequest)
    {
        var category = Category.FromDTO(categoryRequest);

        var result = await _categoryRepository.AddAsync(category);

        if (result != "Ok")
            return BadRequest(result);

        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, CategoryViewModel.FromModel(category));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CategoryDTO categoryRequest)
    {
        var product = Category.FromDTO(categoryRequest);

        var result = await _categoryRepository.UpdateAsync(id, product);

        if (result == "Not Found")
            return NotFound();

        if (result != "Ok")
            return BadRequest(result);

        await _unitOfWork.SaveAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var deleted = await _categoryRepository.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        await _unitOfWork.SaveAsync();

        return Ok();
    }
}
