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

			public DbSet<User> User { get; set; }
			public DbSet<Address> Address { get; set; }
			public DbSet<Currency> Currency { get; set; }
	}
}
