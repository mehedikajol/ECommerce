namespace ECommerce.Web.Middlewares;

internal static class ConfigureIdentityMiddleware
{
    internal static void ConfigureIdentity(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
