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
		public DbSet<tags> Tags { get; set; }
		public DbSet<Category> Category { get; set; }
		public DbSet<Files> Files { get; set; }
		public DbSet<ModelTags> Model_Tag { get; set; }
		public DbSet<QRCode> qr_Code { get; set; }
		public DbSet<Subjects> subjects { get; set; }
		public DbSet<_3DModels> Models3d { get; set; }
					



	}
	
}
