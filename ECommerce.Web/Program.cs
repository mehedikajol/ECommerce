using ECommerce.Web.Extensions;
using ECommerce.Web.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    // Load via service extensions
    builder.ConfigureAllExtensions();
}

try
{
    // Register Request pipelines
    var app = builder.Build();
    Log.Information("----------Application booting.----------");

    // Load via middleware extensions
    app.ConfigureAllMiddlewares(builder);

    app.Run();
}
catch (Exception ex) when (ex is not OperationCanceledException && ex.GetType().Name != "StopTheHostException")
{
    // when (ex is not OperationCanceledException && ex.GetType().Name != "StopTheHostException")
    // is added cause it throws exception while creating miration using IdentityDbContext

    Log.Fatal(ex, "Application failed to load.");
}
finally
{
    Log.CloseAndFlush();
}