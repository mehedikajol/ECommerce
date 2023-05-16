namespace ECommerce.Web.Extensions;

public static class ConfigureCookieExtension
{
    public static void ConfigureCookie(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            // cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

            options.LoginPath = "/Account/Signin";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });
    }
}
