namespace ECommerce.Web.Middlewares;

internal static class ConfigureApplicationMiddlewares
{
    internal static void ConfigureMiddlewares(this IApplicationBuilder app, WebApplicationBuilder builder)
    {
        app.UseHttpsRedirection();
        app.ConfigureStaticFiles(builder);

        app.UseRouting();

        app.ConfigureIdentity();

        app.UseSession();

        app.ConfigureEndpoints();
    }
}
