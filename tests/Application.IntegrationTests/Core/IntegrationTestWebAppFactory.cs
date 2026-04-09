using Application.Abstractions.Users;
using Application.IntegrationTests.Core.Services;
using Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Respawn;
using Testcontainers.MsSql;

namespace Application.IntegrationTests.Core;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest").Build();
    private SqlConnection _connection = null!;
    private Respawner _respawner = null!;
    public string ConnectionString => _container.GetConnectionString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ConnectionString));

            services.RemoveAll<IUserContext>();
            services.AddScoped<IUserContext, FakeUserContext>();
        });
    }

    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync();

        _connection = new(ConnectionString);
        await _connection.OpenAsync();

        using IServiceScope scope = Services.CreateScope();
        ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        _respawner = await Respawner.CreateAsync(
            _connection,
            new RespawnerOptions { DbAdapter = DbAdapter.SqlServer, TablesToIgnore = ["__EFMigrationsHistory"] }
        );
    }

    public async Task ResetAndSeedAsync()
    {
        await _respawner.ResetAsync(_connection);

        using IServiceScope scope = Services.CreateScope();
        DataSeeder dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await dataSeeder.SeedAsync();
    }

    public new async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
        await base.DisposeAsync();
    }
}
