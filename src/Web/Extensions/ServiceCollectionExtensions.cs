using Infrastructure.Authorization;
using Infrastructure.Database;
using Infrastructure.Users;
using Serilog;

namespace Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServices();
        services.AddSerilogServices(configuration);
        services.AddControllersWithViews();
    }

    private static void AddIdentityServices(this IServiceCollection services)
    {
        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }

    private static void AddSerilogServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((services, lc) => lc.ReadFrom.Configuration(configuration).ReadFrom.Services(services));
    }
}
