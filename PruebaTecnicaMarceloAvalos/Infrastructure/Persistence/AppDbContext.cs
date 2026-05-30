using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Domain.Entities;

namespace PruebaTecnicaMarceloAvalos.Infrastructure.Persistence
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
			{
			}

			public DbSet<Users> Users { get; set; }
			public DbSet<Addresses> Addresses { get; set; }
			public DbSet<Currencies> Currencies { get; set; }
	}
}
