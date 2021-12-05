using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Automation_RWassist.Configuration
{
    class ConfigurationReader
    {
        readonly IConfiguration configuration;

        public ConfigurationReader(string path)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(path);
            configuration = builder.Build();

        }

        public object GetConfiguration(string configurationName)
        {
            return configuration.GetSection(configurationName).Value;
        }
    }
}
