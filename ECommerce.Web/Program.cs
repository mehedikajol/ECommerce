using Autofac.Extensions.DependencyInjection;
using ECommerce.Web.Extensions;
using ECommerce.Web.Helpers;
using Microsoft.Extensions.FileProviders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    // Load settings via extensions
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

try
{
    // Register Request pipelines
    var app = builder.Build();

    app.Services.GetAutofacRoot();
    Log.Information("----------Application booting.----------");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    AdminDataSeeder.LoadAdminDataAndRole(app.Services).Wait();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(builder.Configuration.GetSection("FileStorageSettings:FileDirectory").Value),
        RequestPath = builder.Configuration.GetSection("FileStorageSettings:DirectoryName").Value
    });

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseSession();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
    });

    app.Run();
}
catch (Exception ex) when (ex is not OperationCanceledException && ex.GetType().Name != "StopTheHostException")
{
    // when (ex is not OperationCanceledException && ex.GetType().Name != "StopTheHostException")
    // is added cause it throws exception while creating miration using IdentityDbContext

    Log.Fatal(ex, "Application failed to load.");
}
finally
{
    Log.CloseAndFlush();
}