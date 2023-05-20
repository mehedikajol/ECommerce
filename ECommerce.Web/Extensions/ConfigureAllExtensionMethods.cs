namespace ECommerce.Web.Extensions;

internal static class ConfigureAllExtensionMethods
{
    internal static void ConfigureAllExtensions(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureDbContext(builder); // AppDbContext configuration
        builder.Services.ConfigureIdentity(); // Identity configuration
        builder.Services.ConfigureCommonClasses(builder); // Common classes configuration
        builder.Host.ConfigureAutofac(); // Using Autofac as dependency container
        builder.Host.ConfigureLogger(); // Logger
        builder.Services.ConfigureCookie(); // Cookie configuration
        builder.Services.ConfigureSession(); // Session Configuration

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddRazorPages();
    }
}
