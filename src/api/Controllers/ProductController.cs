using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Repository;

namespace ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet("status")]
        public async Task<IActionResult> Status()
        {
            return await Task.FromResult(Ok("Hello world!"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAll();

            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var products = await _productRepository.GetByName(name);

            if (!products.Any())
                return NoContent();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductModel product)
        {
            if (!await _productRepository.Add(product))
                return BadRequest();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductModel product)
        {
            if (await _productRepository.GetById(id) == null)
                return NotFound();

            if (!await _productRepository.Update(id, product))
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!await _productRepository.Delete(id))
                return NotFound();

            return Ok();
        }
    }
}
