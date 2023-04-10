using Autofac;
using Autofac.Extensions.DependencyInjection;
using ECommerce.Web;
using ECommerce.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var migrationAssemblyName = Assembly.GetExecutingAssembly().FullName;

    // Use DbContextPool for better performance
    builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString)); // Use Sqlite for my Potato pc

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();

    // Using Autofac as dependency container
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(options =>
    {
        // register modules here
        options.RegisterModule(new WebModule());
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
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(builder.Configuration));
    builder.Logging.ClearProviders();

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddControllersWithViews();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddRazorPages();
}

try{
    // Register Request pipelines
    var app = builder.Build();

    app.Services.GetAutofacRoot();
    Log.Error("Application booting.");

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

    app.UseHttpsRedirection();
    app.UseStaticFiles();

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
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to boot!");
}
finally
{
    Log.CloseAndFlush();
}