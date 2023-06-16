using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Web.StartupConfigurations
{
    public class AngularSpaConfiguration : IStartupConfiguration
    {
        private readonly string spaPath;

        public AngularSpaConfiguration(string spaPath = "ClientApp/dist")
        {
            this.spaPath = spaPath;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSpaStaticFiles(c => c.RootPath = spaPath);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                    spa.UseAngularCliServer("start");
            });
        }
    }
}
