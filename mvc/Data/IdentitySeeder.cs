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


            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }

            string adminEmail = "admin@site.com";
            string adminPassword = "Admin123!";


            if(await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };


                await userManager.CreateAsync(
                    admin,
                    adminPassword);


                await userManager.AddToRoleAsync(
                    admin,
                    "Admin");
            }

            string userEmail = "user@site.com";
            string userPassword = "User123!";


            if(await userManager.FindByEmailAsync(userEmail) == null)
            {
                var user = new IdentityUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };


                await userManager.CreateAsync(
                    user,
                    userPassword);


                await userManager.AddToRoleAsync(
                    user,
                    "User");
            }
        }
    }
}
