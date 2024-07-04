using Microsoft.EntityFrameworkCore;
using Product.Domain.Models;

namespace Product.Infrastructure.Data
{
    public class ApplicationDataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ProductModel> Products { get; set; }
    }
}
