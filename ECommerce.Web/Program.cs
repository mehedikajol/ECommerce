using Autofac;
using Autofac.Extensions.DependencyInjection;
using ECommerce.Core.Common;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Context;
using ECommerce.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var migrationAssemblyName = Assembly.GetExecutingAssembly().FullName;

    builder.Services.AddDbContextPool<AppDbContext>(options =>
        options.UseSqlite(connectionString));

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<AppDbContext>();

    builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

    // Using Autofac as dependency container
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(options =>
    {
        // register modules here
        options.RegisterModule(new WebModule());
        options.RegisterModule(new InfrastructureModule());
    });

    // Cookie configuration
    builder.Services.ConfigureApplicationCookie(options =>
    {
        // cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

        options.LoginPath = "/Account/Signin";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
    });

    // Session Configuration
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromSeconds(100);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

    // Recommended to use this logger
    builder.Host.UseSerilog((context, config) => config
        .ReadFrom.Configuration(context.Configuration));

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
    Log.Information("Application booting.");

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
    var filepath = builder.Configuration.GetValue<string>("ApplicationSettings:FileDirectory");

    app.UseHttpsRedirection();
    app.UseStaticFiles( new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(filepath, "")),
        RequestPath = "/"+filepath
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