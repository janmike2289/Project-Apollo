using Apollo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apollo.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<CMItemEntity> CMItems => Set<CMItemEntity>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		// base.OnModelCreating(modelBuilder);

		//Override the OnModelCreating method in your context class and use ToTable() to explicitly define the existing table's name
		modelBuilder.Entity<CMItemEntity>().ToTable("CMItems");
	}
}

