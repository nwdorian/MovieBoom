using System.Globalization;
using Infrastructure;
using Serilog;
using Web.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting application...");

    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Services.AddPresentation(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.ConfigureServices();

    WebApplication app = builder.Build();

    app.UseWebApplicationMiddleware();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}
