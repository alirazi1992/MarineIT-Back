using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MarineIT.Infrastructure.Data
{
    // Used by EF Core Tools at design-time (PMC, dotnet ef)
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(
                    // Keep in sync with appsettings.json
                    "Server=(localdb)\\MSSQLLocalDB;Database=MarineITDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
                )
                .Options;

            return new AppDbContext(options);
        }
    }
}
