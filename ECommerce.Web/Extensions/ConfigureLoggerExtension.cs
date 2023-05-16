using Serilog;

namespace ECommerce.Web.Extensions;

internal static class ConfigureLoggerExtension
{
    internal static void ConfigureLogger(this IHostBuilder host)
    {
        host.UseSerilog((context, config) => config
            .ReadFrom.Configuration(context.Configuration));
    }
}
