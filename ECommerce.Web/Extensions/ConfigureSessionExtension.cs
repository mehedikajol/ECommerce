namespace ECommerce.Web.Extensions;

public static class ConfigureSessionExtension
{
    public static void ConfigureSession(this IServiceCollection services)
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
