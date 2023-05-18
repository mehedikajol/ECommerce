namespace ECommerce.Web.Extensions;

internal static class ConfigureCookieExtension
{
    internal static void ConfigureCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            // cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromSeconds(100);

            options.LoginPath = "/Account/Signin";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });
    }
}
