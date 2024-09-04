using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public static class Configuration
    {
        private static readonly IConfigurationRoot ConfigurationRoot;

        static Configuration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory()); // Set base path for the JSON file
            configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Add the JSON configuration file

            ConfigurationRoot = configurationBuilder.Build();
        }

        public static string ConnectionString
        {
            get
            {
                return ConfigurationRoot.GetConnectionString("SqlServer");
            }
        }
    }
}
