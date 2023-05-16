using Serilog;

namespace ECommerce.Web.Extensions;

public static class ConfigureLoggerExtension
{
    public static void ConfigureLogger(this IHostBuilder host)
    {
        host.UseSerilog((context, config) => config
        .ReadFrom.Configuration(context.Configuration));
    }
}
