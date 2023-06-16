using Xunit;

namespace AppTemplate.Users.Tests.TestServices
{
    [CollectionDefinition(Name)]
    public class FixtureCollection
        : ICollectionFixture<TestApplicationFactory>
    {
        public const string Name = "FixtureCollection";
    }
}
