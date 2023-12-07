using Microsoft.Extensions.Configuration;
using System.IO;

namespace ReDoMusic.Infrastructure.Persistence
{
    public static class Configurations
    {
        public static string GetString(string key)
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectDirectory)
                .AddJsonFile("appsettings.json")
                .Build();


            return configuration.GetSection(key).Value;
        }
    }
}
