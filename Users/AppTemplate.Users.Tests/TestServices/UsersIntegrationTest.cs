using AppTemplate.App.Database.Migrations;
using AppTemplate.App.Web;
using AppTemplate.Users.Database;
using Core.Tests;
using Xunit;

namespace AppTemplate.Users.Tests.TestServices
{
    [Collection(FixtureCollection.Name)]
    public abstract class UsersIntegrationTest : IntegrationTest<UsersDataContext, Startup, MigrationScripts>
    {
        public UsersIntegrationTest(TestApplicationFactory factory) : base(factory)
        {
        }
    }
}
