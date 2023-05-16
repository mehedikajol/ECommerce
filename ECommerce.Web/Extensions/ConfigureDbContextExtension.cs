using ECommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Web.Extensions;

internal static class ConfigureDbContextExtension
{
    internal static void ConfigureDbContext(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContextPool<AppDbContext>(options =>
            options.UseSqlite(
                connectionString,
                ctx => ctx.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
    }
}
