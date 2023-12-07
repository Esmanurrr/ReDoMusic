using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ReDoMusic.Infrastructure.Persistence.Contexts;
using System.IO;

namespace ReDoMusic.Persistence.Contexts
{
    public class ReDoDbContextFactory : IDesignTimeDbContextFactory<ReDoMusicDbContext>
    {
        public ReDoMusicDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ReDoMusicDbContext>();

            var connectionString = configuration.GetConnectionString("PostgreSQLConnection");

            optionsBuilder.UseNpgsql(connectionString);

            return new ReDoMusicDbContext(optionsBuilder.Options);
        }
    }
}
