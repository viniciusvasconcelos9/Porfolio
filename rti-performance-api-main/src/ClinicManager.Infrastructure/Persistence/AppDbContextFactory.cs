using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClinicManager.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configurationPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "/Users/thayronemarquessilva/Documents/Projects/RTI Cardioperformance/rti-performance-api/src/ClinicManager.API", "appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configurationPath, optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            //var connection = "Host=dpg-cr147ga3esus73aqg3p0-a.oregon-postgres.render.com;Database=rti_cardioperformance_database;Username=rti_cardioperformance_database_user;Password=MUovfaGHsyqxXJlYdemlqqKxgg5O1bAL";
            Console.WriteLine($"Connection String: {connectionString}");
            optionsBuilder.UseNpgsql(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}