using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Core;

[Collection(nameof(SharedTestCollection))]
public abstract class BaseIntegrationTest(IntegrationTestWebAppFactory factory) : IAsyncLifetime
{
    private readonly AsyncServiceScope _scope = factory.Services.CreateAsyncScope();
    private readonly IntegrationTestWebAppFactory _factory = factory;
    private ApplicationDbContext _dbContext = null!;
    protected AsyncServiceScope Scope => _scope;
    protected ApplicationDbContext DbContext => _dbContext;

    public async ValueTask InitializeAsync()
    {
        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await _factory.ResetAndSeedAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _scope.DisposeAsync();
        await _dbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
