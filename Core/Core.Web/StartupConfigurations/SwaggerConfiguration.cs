using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Core.Web.StartupConfigurations
{
    public class SwaggerConfiguration : IStartupConfiguration
    {
        private readonly string applicationName;
        private readonly string version;

        public SwaggerConfiguration(string applicationName, string version)
        {
            this.applicationName = applicationName;
            this.version = version;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var apiInfo = new OpenApiInfo {Title = applicationName, Version = version};
            services.AddSwaggerGen(c => c.SwaggerDoc(version, apiInfo));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{applicationName} API {version}"));
        }
    }
}
