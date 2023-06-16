using Core.App.CurrentUser;
using Core.Web.DependencyInjection;
using Core.Web.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Web.StartupConfigurations
{
    public class WebCoreConfiguration : IStartupConfiguration
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterModule<CurrentUserModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
