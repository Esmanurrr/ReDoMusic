using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Persistence.Contexts
{
    public class ReDoIdentityContextFactory : IDesignTimeDbContextFactory<ReDoIdentityContext>
    {
        public ReDoIdentityContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ReDoIdentityContext>();

            var connectionString = configuration.GetSection("PostgreSQL").Value;

            optionsBuilder.UseNpgsql(connectionString);

            return new ReDoIdentityContext(optionsBuilder.Options);
        }
    }
}
