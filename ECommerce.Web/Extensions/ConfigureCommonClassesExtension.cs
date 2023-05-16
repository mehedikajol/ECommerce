using ECommerce.Core.Common;

namespace ECommerce.Web.Extensions;

public static class ConfigureCommonClassesExtension
{
    public static void ConfigureCommonClasses(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.Configure<FileStorageSettings>(builder.Configuration.GetSection("FileStorageSettings"));
    }
}
