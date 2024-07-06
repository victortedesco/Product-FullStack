using Microsoft.AspNetCore.Mvc;
using Products.API.ViewModels;
using Products.Domain.DTOs;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Repository;

namespace Products.API.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = productRepository.UnitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productRepository.GetAllAsync();

        if (!products.Any())
            return NoContent();

        return Ok(products.Select(p => ProductViewModel.FromModel(p)));
    }

    [HttpGet("id/{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
            return NotFound();

        return Ok(ProductViewModel.FromModel(product));
    }

    [HttpGet("name/{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var products = await _productRepository.GetByNameAsync(name);

        if (!products.Any())
            return NoContent();

        return Ok(products.Select(p => ProductViewModel.FromModel(p)));
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody] ProductDTO productRequest)
    {
        var product = Product.FromDTO(productRequest);

        var result = await _productRepository.AddAsync(product);

        if (result != "Ok")
            return BadRequest(result);

        await _unitOfWork.SaveAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductViewModel.FromModel(product));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductDTO productRequest)
    {
        var product = Product.FromDTO(productRequest);

        var result = await _productRepository.UpdateAsync(id, product);

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
        var deleted = await _productRepository.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        await _unitOfWork.SaveAsync();

        return Ok();
    }
}
