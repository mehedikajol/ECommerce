using Microsoft.AspNetCore.Identity;
using Serilog;

namespace ECommerce.Web.Helpers;

public static class AdminDataSeeder
{
    public static async Task LoadAdminDataAndRole(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        string roleName = "SuperAdmin";
        IdentityResult roleResult;
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if(!roleExists)
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));

        var email = "superadmin@email.com";
        var password = "P@ssw0rd";
        if(userManager.FindByEmailAsync(email).Result == null)
        {
            IdentityUser user = new()
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true,
            };
            IdentityResult userResult = userManager.CreateAsync(user, password).Result;
            if (userResult.Succeeded)
                userManager.AddToRoleAsync(user, roleName).Wait();

            Log.Information("Admin data seeding complete.");
        }
    }
}
