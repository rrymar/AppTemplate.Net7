using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Web.StartupConfigurations
{
    public class DatabaseMigrationConfiguration<T> : IStartupConfiguration
        where T: DbContext
    {
        public void ConfigureMigrationServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<T>(o => o.UseSqlServer("Server=.;Database=dummy"));

        }
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
        }
    }
}
