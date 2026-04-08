using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Core;

[Collection(nameof(SharedTestCollection))]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private readonly IServiceScope _scope;
    protected readonly IntegrationTestWebAppFactory Factory;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Factory = factory;
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    public async ValueTask InitializeAsync()
    {
        await Factory.ResetAndSeedAsync();
    }

    public ValueTask DisposeAsync()
    {
        _scope.Dispose();
        return ValueTask.CompletedTask;
    }
}
