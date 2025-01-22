using GizWebApp.Model;
using GizWebApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GizWebApp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IProductRepository _productRepository;

		public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var products = await _productRepository.GetAllAsync();
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var product = await _productRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Product product)
		{
			await _productRepository.AddAsync(product);
			return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}

			var updated = await _productRepository.UpdateAsync(product);
			if (!updated)
			{
				return NotFound();
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var deleted = await _productRepository.DeleteAsync(id);
			if (!deleted)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
