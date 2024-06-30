using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Data
{
    public class ApplicationDataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ProductModel> Products { get; set; }
    }
}
