using System.Globalization;
using Application.Abstractions.Emails;
using Infrastructure.Authorization;
using Infrastructure.Database;
using Infrastructure.Emails;
using Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Serilog;

namespace Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.SetupLocalization();
        services.AddIdentityServices();
        services.AddSerilogServices(configuration);
        services.AddControllersWithViews();
        services.AddEmailServices();
    }

    private static void AddIdentityServices(this IServiceCollection services)
    {
        services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }

    private static void AddSerilogServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((services, lc) => lc.ReadFrom.Configuration(configuration).ReadFrom.Services(services));
    }

    private static void AddEmailServices(this IServiceCollection services)
    {
        services
            .AddOptions<SmtpSettings>()
            .BindConfiguration(SmtpSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddScoped<IEmailService>(sp =>
        {
            SmtpSettings settings = sp.GetRequiredService<IOptions<SmtpSettings>>().Value;
            return new EmailService(settings);
        });
    }

    private static void SetupLocalization(this IServiceCollection services)
    {
        CultureInfo culture = new("en-US", useUserOverride: false);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
