using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistence;
using CarWorkshop.Infrastructure.Repositories;
using CarWorkshop.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarWorkshop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarWorkshopDbContext>(options => options.UseSqlite(configuration.GetConnectionString("CarWorkshop"))); //Pobieranie connStringa z pliku appsetingJson;

            services.AddScoped<CarWorkshopSeeder>();
            services.AddScoped<ICarWorkshopRepository, CarWorkshopRepository>();
            services.AddScoped<ICarWorkshopServiceRepository, CarWorkshopServiceRepository>();

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CarWorkshopDbContext>();
        }
    }
}
