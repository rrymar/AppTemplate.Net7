using System.Collections.Generic;
using AppTemplate.App.Database;
using AppTemplate.Users;
using Core.Web.StartupConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.App.Web
{
    public class Startup
    {
        private readonly List<IStartupConfiguration> startupConfigurations = new();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            startupConfigurations.Add(new SwaggerConfiguration("AppTemplate", "v1"));
            startupConfigurations.Add(new ApplicationInsightsConfiguration());
            startupConfigurations.Add(new WebCoreConfiguration());

            startupConfigurations.Add(new AspNetConfiguration(new []
            {
                new UsersModule()
            }));

            startupConfigurations.Add(new AngularSpaConfiguration());

            startupConfigurations.Add(new DatabaseMigrationConfiguration<DataContext>());
        }

        private IConfiguration Configuration { get; }

        public void ConfigureMigrationServices(IServiceCollection services)
        {
            startupConfigurations.ConfigureMigrationServices(services, Configuration);
            ConfigureServices(services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            startupConfigurations.ConfigureServices(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            startupConfigurations.Configure(app, env, Configuration);
        }
    }
}
