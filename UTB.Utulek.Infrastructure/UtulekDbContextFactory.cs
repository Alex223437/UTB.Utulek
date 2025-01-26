using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace UTB.Utulek.Infrastructure.Database
{
    public class UtulekDbContextFactory : IDesignTimeDbContextFactory<UtulekDbContext>
    {
        public UtulekDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UtulekDbContext>();

            // Загрузка строки подключения из appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MySql");

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32)));

            return new UtulekDbContext(optionsBuilder.Options);
        }
    }
}