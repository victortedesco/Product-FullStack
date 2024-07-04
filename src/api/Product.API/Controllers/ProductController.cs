using Microsoft.AspNetCore.Mvc;
using Product.Domain.Interfaces.Repository;
using Product.Domain.Models;

namespace Product.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAll();

            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(IEnumerable<ProductModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var products = await _productRepository.GetByName(name);

            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProductModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] ProductModel product)
        {
            if (!await _productRepository.Add(product))
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductModel product)
        {
            if (await _productRepository.GetById(id) == null)
                return NotFound();

            if (!await _productRepository.Update(id, product))
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!await _productRepository.Delete(id))
                return NotFound();

            return Ok();
        }
    }
}
