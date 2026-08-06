using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MvcSample.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
           
            string[] roles =
            {
                "Admin",
                "User"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }

            string adminEmail = "admin@site.com";
            string adminPassword = "Admin123!";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(
                    admin,
                    adminPassword);
            }
            else
            {
                admin.UserName = "Admin";
                admin.EmailConfirmed = true;

                await userManager.UpdateAsync(admin);
            }

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                await userManager.AddToRoleAsync(
                    admin,
                    "Admin");
            }

            string userEmail = "user@site.com";
            string userPassword = "User123!";

            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "Default User",
                    Email = userEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(
                    user,
                    userPassword);
            }
            else
            {
                user.UserName = "Default User";
                user.EmailConfirmed = true;

                await userManager.UpdateAsync(user);
            }

            if (!await userManager.IsInRoleAsync(user, "User"))
            {
                await userManager.AddToRoleAsync(
                    user,
                    "User");
            }
        }
    }
}
