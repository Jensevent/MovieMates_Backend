using Xunit;

namespace MovieMates_Backend_IntegrationTests.Shared
{
    [CollectionDefinition("Tests")]
    public class DatabaseCollection : ICollectionFixture<DatabaseSetup>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
