using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; private set; }
    public DbSet<Category> Categories { get; private set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=Products.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
    }
}
