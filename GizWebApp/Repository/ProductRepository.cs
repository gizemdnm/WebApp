using GizWebApp.Data;
using GizWebApp.Model;
using Microsoft.EntityFrameworkCore;


namespace GizWebApp.Repository
{
	public class ProductRepository
	{
		private readonly AppDbContext _appDbContext;

		public ProductRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<Product> GetEmpById(int id)
		{
			return await _appDbContext.Products.FindAsync(id);
		}
		public async Task<List<Product>> GetProducts()
		{
			return await _appDbContext.Products.ToListAsync();
		}

		// Tek bir ürün ID'sine göre getirir.
		public async Task<Product> GetByIdAsync(int id)
		{
			return await _appDbContext.Products.FindAsync(id);
		}

		// Yeni ürün ekler.
		public async Task SaveEmp(Product product)
		{
			await _appDbContext.Products.AddAsync(product);
			await _appDbContext.SaveChangesAsync();
		}

		// Ürün siler.
		public async Task DeleteEmp(int id)
		{
			var emp = await _appDbContext.Products.FindAsync(id);
			if (emp != null)
			{
				_appDbContext.Products.Remove(emp);
				await _appDbContext.SaveChangesAsync();
			}
		}

		// Ürünü günceller.
		public async Task UpdateEmp(Product product)
		{
			var existingEmp = await _appDbContext.Products.FindAsync(product.Id);
			if (existingEmp != null)
			{
				existingEmp.Name = product.Name;
				existingEmp.Price = product.Price;

				await _appDbContext.SaveChangesAsync();
			}
		}

		// Tüm ürünleri listelemek için metot.
		public async Task<List<Product>> GetAllAsync()
		{
			return await _appDbContext.Products.ToListAsync();
		}
	}
}
