namespace Application.IntegrationTests.Core;

[CollectionDefinition(nameof(SharedTestCollection))]
public class SharedTestCollection : ICollectionFixture<IntegrationTestWebAppFactory> { }
