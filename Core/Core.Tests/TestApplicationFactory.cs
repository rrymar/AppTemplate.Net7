using Core.Tests.Database;
using Core.Web.WebClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;

namespace Core.Tests
{
    public class TestApplicationFactory<TDbContext, TStartup, TMigrationScripts> : WebApplicationFactory<TStartup>
        where TDbContext : DbContext
        where TStartup : class
    {
        public virtual List<ITestMigration<TDbContext>> TestMigrations
            => new List<ITestMigration<TDbContext>>();

        public IHttpClient CreateTestClient()
        {
            return new TestingHttpClient(CreateClient());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            builder.ConfigureAppConfiguration((_, c) => c.AddJsonFile(configPath));

            builder.ConfigureTestServices(s =>
            {
                var provider = s.BuildServiceProvider();
                var db = provider.GetRequiredService<TDbContext>();
                db.InitTestDatabases(typeof(TMigrationScripts).Assembly, TestMigrations);
                provider.Dispose();

                ConfigureTestServices(s);
            });
        }

        protected virtual void ConfigureTestServices(IServiceCollection services)
        {
            services.AddScoped(_ => new RestClient(CreateTestClient()));
        }
    }
}
