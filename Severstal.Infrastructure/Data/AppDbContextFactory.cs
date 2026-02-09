using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Severstal.Infrastructure.Data
{
    internal sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Severstal.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            optionsBuilder.UseNpgsql(connectionString,
                m => m.MigrationsAssembly("Severstal.Infrastructure"));
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
