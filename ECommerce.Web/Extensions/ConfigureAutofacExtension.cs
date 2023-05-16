using Autofac;
using Autofac.Extensions.DependencyInjection;
using ECommerce.Infrastructure;

namespace ECommerce.Web.Extensions;

internal static class ConfigureAutofacExtension
{
    internal static void ConfigureAutofac(this IHostBuilder host)
    {
        host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        host.ConfigureContainer<ContainerBuilder>(options =>
        {
            // register modules here
            options.RegisterModule(new WebModule());
            options.RegisterModule(new InfrastructureModule());
        });
    }
}
