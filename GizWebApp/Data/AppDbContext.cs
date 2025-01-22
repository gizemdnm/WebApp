using GizWebApp.Model;
using Microsoft.EntityFrameworkCore;

namespace GizWebApp.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<Product> Products {  get; set; } 
		public AppDbContext(DbContextOptions options) : base(options)
		{ }
		public Product ProductId { get; set; }
		public object Name { get; set; }
		public object Price { get; set; }

		public object Product { get; set; }
		
	}
}
