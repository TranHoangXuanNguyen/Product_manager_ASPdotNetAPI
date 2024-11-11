using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductRepository.DbContextFactory
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProductDBContext>
    {
        public ProductDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductDBContext>();

            // Specify your connection string here
            optionsBuilder.UseSqlServer("Server=DESKTOP-L7F9546\\SQLEXPRESS;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True");

            return new ProductDBContext(optionsBuilder.Options);
        }
    }
}
