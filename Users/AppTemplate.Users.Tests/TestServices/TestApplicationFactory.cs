using System.Collections.Generic;
using AppTemplate.App.Database.Migrations;
using AppTemplate.App.Web;
using AppTemplate.Users.Database;
using AppTemplate.Users.Tests.TestServices.TestMigrations;
using Core.Tests;
using Core.Tests.Database;
using Core.Web.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Users.Tests.TestServices
{
    public class TestApplicationFactory : TestApplicationFactory<UsersDataContext, Startup, MigrationScripts>
    {
        public override List<ITestMigration<UsersDataContext>> TestMigrations 
            => UsersTestMigrations.Migrations;

        protected override void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureTestServices(services);
            services.RegisterModule<UserTestServicesModule>();
        }
    }
}
