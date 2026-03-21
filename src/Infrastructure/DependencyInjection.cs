using Application.Abstractions;
using Application.Abstractions.Database;
using Infrastructure.Authentication;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddIdentityStore();
        services.AddScoped<IUserService, UserService>();
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString =
            configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Connection string 'Default' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }

    private static void AddIdentityStore(this IServiceCollection services)
    {
        services
            .AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
                options.SignIn.RequireConfirmedAccount = false
            )
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}
