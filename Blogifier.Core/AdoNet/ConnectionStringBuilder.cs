using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogifier.Core.AdoNet
{
    public class ConnectionStringBuilder
    {
        public static string GetConnectionString(string cnnName = "DefaultConnection")
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var section = config.GetSection("ConnectionStrings");
            var connectionString = section.GetValue<string>(cnnName);

            return connectionString;
        }

        public static string ConnectionString => GetConnectionString();
    }
}
