﻿using ECommerce.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Web.Extensions;

internal static class ConfigureIdentityExtension
{
    internal static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            //options.SignIn.RequireConfirmedAccount = true;
        })
        .AddDefaultUI()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
    }
}
