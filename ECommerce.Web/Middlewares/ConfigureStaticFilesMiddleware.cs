using Microsoft.Extensions.FileProviders;

namespace ECommerce.Web.Middlewares;

internal static class ConfigureStaticFilesMiddleware
{
    internal static void ConfigureStaticFiles(this IApplicationBuilder app, WebApplicationBuilder builder)
    {
        app.UseStaticFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(builder.Configuration.GetSection("FileStorageSettings:FileDirectory").Value),
            RequestPath = builder.Configuration.GetSection("FileStorageSettings:DirectoryName").Value
        });
    }
}
