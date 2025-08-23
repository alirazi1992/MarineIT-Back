using MarineIT.Application.Tickets;
using MarineIT.Application.Vessels;
using MarineIT.Infrastructure.Data;
using MarineIT.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MarineIT.Infrastructure.Setup
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var conn = config.GetConnectionString("Default")
                ?? "Server=(localdb)\\MSSQLLocalDB;Database=MarineITDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conn));

            // only app services here
            services.AddScoped<IVesselService, VesselService>();
            services.AddScoped<ITicketService, TicketService>();

            return services;
        }
    }
}
