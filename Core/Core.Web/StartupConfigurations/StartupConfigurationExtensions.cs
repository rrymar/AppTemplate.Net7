using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Web.StartupConfigurations
{
    public static class StartupConfigurationExtensions
    {
        public static void ConfigureServices(this IEnumerable<IStartupConfiguration> startupConfigurations,
            IServiceCollection services, IConfiguration configuration)
        {
            foreach (var startupConfiguration in startupConfigurations)
            {
                startupConfiguration.ConfigureServices(services, configuration);
            }
        }

        public static void ConfigureMigrationServices(this IEnumerable<IStartupConfiguration> startupConfigurations,
            IServiceCollection services, IConfiguration configuration)
        {
            foreach (var startupConfiguration in startupConfigurations)
            {
                startupConfiguration.ConfigureMigrationServices(services, configuration);
            }
        }

        public static void Configure(this IEnumerable<IStartupConfiguration> startupConfigurations,
            IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            foreach (var startupConfiguration in startupConfigurations)
            {
                startupConfiguration.Configure(app, env, configuration);
            }
        }
    }
}
