using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace ECommerce.Web.Helpers;

internal static class AdminDataSeeder
{
    internal static async Task LoadAdminDataAndRole(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var userService = serviceProvider.GetRequiredService<IUserProfileService>();

        string roleName = "SuperAdmin";
        IdentityResult roleResult;
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));

        var email = "superadmin@email.com";
        var password = "P@ssw0rd";
        if (userManager.FindByEmailAsync(email).Result == null)
        {
            IdentityUser user = new()
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true,
            };
            IdentityResult userResult = userManager.CreateAsync(user, password).Result;
            await userService.CreateUserProfile(new UserProfile
            {
                UserId = new Guid(user.Id),
                FirstName = "Super",
                LastName = "Admin",
                Address = "",
                Gender = 1,
                Email = user.Email,
                InsertedBy = user.Email,
                ProfilePictureUrl = ""
            });

            if (userResult.Succeeded)
                userManager.AddToRoleAsync(user, roleName).Wait();

            Log.Information("Admin data seeding complete.");
        }
    }
}
