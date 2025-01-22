using GizWebApp.Model;
using GizWebApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GizWebApp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly ProductRepository _productRepository;

		public ProductController(ProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[HttpGet]
		public async Task<ActionResult> GetProducts()
		{
			try
			{
				var products = await _productRepository.GetProducts();
				return Ok(new { Success = true, Message = "Products retrieved successfully.", Data = products });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Success = false, Message = "An error occurred.", Details = ex.Message });
			}
		}

		[HttpPost]
		public async Task<ActionResult> AddProduct(Product product)
		{
			try
			{
				await _productRepository.SaveEmp(product);
				return Ok(new { Success = true, Message = "Product added successfully." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Success = false, Message = "An error occurred.", Details = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteProduct(int id)
		{
			try
			{
				var emp = await _productRepository.GetEmpById(id);
				if (emp == null)
				{
					return NotFound(new { Success = false, Message = "Product not found." });
				}
				await _productRepository.DeleteEmp(id);
				return Ok(new { Success = true, Message = "Product deleted successfully." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Success = false, Message = "An error occurred.", Details = ex.Message });
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest(new { Success = false, Message = "Product ID mismatch." });
			}

			var existingProduct = await _productRepository.GetEmpById(id);
			if (existingProduct == null)
			{
				return NotFound(new { Success = false, Message = "Product not found." });
			}

			try
			{
				await _productRepository.UpdateEmp(product);
				return Ok(new { Success = true, Message = "Product updated successfully." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Success = false, Message = "An error occurred.", Details = ex.Message });
			}
		}
	}
}