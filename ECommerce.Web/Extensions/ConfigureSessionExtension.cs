namespace ECommerce.Web.Extensions;

internal static class ConfigureSessionExtension
{
    internal static void ConfigureSession(this IServiceCollection services)
    {
        // Configure session
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(100);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }
}
