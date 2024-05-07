using Blazor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blazor.Data
{
	public class AppDbContext : DbContext
	{

		private readonly IConfiguration Configuration;
		public AppDbContext(IConfiguration configuration)
	{
		Configuration= configuration;
	}
	
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
		}
		public DbSet<Users> ARLibrary { get; set; }
	}
	
}
