using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class Configuration
    {
        static public string? ConnectionString
        {
            get
            {
                IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // appsettings.json dosyasının bulunduğu dizin
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

                return configuration.GetConnectionString("PostgreSQL");
            }
        }
    }
}
