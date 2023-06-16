using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Web.StartupConfigurations
{
    public interface IStartupConfiguration
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        void ConfigureMigrationServices(IServiceCollection services, IConfiguration configuration)
        {

        }

        void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration);
    }
}
