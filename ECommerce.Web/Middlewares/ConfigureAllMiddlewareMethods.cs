using Autofac.Extensions.DependencyInjection;
using ECommerce.Web.Helpers;

namespace ECommerce.Web.Middlewares;

internal static class ConfigureAllMiddlewareMethods
{
    internal static void ConfigureAllMiddlewares(this WebApplication app, WebApplicationBuilder builder)
    {
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        app.Services.GetAutofacRoot();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error"); // /Home/Error
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        AdminDataSeeder.LoadAdminDataAndRole(app.Services).Wait();

        app.UseHttpsRedirection();
        app.ConfigureStaticFiles(builder);

        app.UseRouting();

        app.ConfigureIdentity();

        app.UseSession();

        app.ConfigureEndpoints();
    }
}
