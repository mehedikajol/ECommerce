using ECommerce.Core.Common;

namespace ECommerce.Web.Extensions;

internal static class ConfigureCommonClassesExtension
{
    internal static void ConfigureCommonClasses(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<FileStorageSettings>(builder.Configuration.GetSection("FileStorageSettings"));
    }
}
