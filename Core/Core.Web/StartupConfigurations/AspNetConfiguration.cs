using System.Collections.Generic;
using Core.Web.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Web.StartupConfigurations
{
    public class AspNetConfiguration : IStartupConfiguration
    {
        private readonly IEnumerable<ITopLevelModule> modules;

        public AspNetConfiguration(IEnumerable<ITopLevelModule> modules)
        {
            this.modules = modules;
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var builder = services.AddControllers();
            foreach (var module in modules)
            {
                services.RegisterTopLevelModule(module, builder, configuration);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (!env.IsDevelopment())
                app.UseHsts();


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
                app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
