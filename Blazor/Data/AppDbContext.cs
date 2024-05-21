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
		public DbSet<Users> Users { get; set; }
		public DbSet<Tags> Tags { get; set; }
		public DbSet<Categories> Category { get; set; }
		public DbSet<Files> File { get; set; }
		public DbSet<model_tags> Model_Tag { get; set; }
		public DbSet<qr_code> qr_Code { get; set; }
		public DbSet<Subject> subjects { get; set; }



	}
	
}
