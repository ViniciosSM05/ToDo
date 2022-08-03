using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DDDApi.Infra.Data.Context
{
    public class EntityContextFactory : IDesignTimeDbContextFactory<EntityContext>
    {
        public EntityContext CreateDbContext(string[] args)
            => Create(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

        public static EntityContext Create()
        {
            var environmentName = Environment.GetEnvironmentVariable("Hosting:Environment");
            var basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        private static EntityContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var connstr = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connstr))
                throw new InvalidOperationException("Could not find a connection string named '(default)'.");

            return Create(connstr);
        }

        private static EntityContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<EntityContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new EntityContext(optionsBuilder.Options);
        }
    }
}
