using System.Collections.Generic;
using AppTemplate.Users.Database;
using Core.Tests.Database;

namespace AppTemplate.Users.Tests.TestServices.TestMigrations
{
    public static class UsersTestMigrations
    {
        public static readonly List<ITestMigration<UsersDataContext>> Migrations = new List<ITestMigration<UsersDataContext>>
        {
            new AddTestUsers()
        };
    }
}
