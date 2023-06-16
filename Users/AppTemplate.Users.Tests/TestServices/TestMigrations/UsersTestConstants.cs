using Core.Database;

namespace AppTemplate.Users.Tests.TestServices.TestMigrations
{
    public static class UsersTestConstants
    {
        public static readonly TestUser User = new TestUser()
        {
            Id = 2,
            Username = "user@rr.com",
        };

        public static TestUser Reviewer => User;

        public static readonly TestUser InactiveUser = new TestUser()
        {
            Id = 3,
            Username = "ina@rr.com",
        };

        public static readonly TestUser System = new TestUser()
        {
            Id = KnownUsers.System,
            Username = "system",
        };
    }
}
