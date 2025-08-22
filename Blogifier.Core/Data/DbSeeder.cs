using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blogifier.Core.Data
{
    public static class DbSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                string[] roleNames = { "Admin", "Manager", "User" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

              
                var adminEmail = "admin@demo.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, "Admin@123"); // pass mặc định

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }

                var manaEmail = "manager@demo.com";
                var manaUser = await userManager.FindByEmailAsync(manaEmail);

                if (manaUser == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "manager",
                        Email = manaEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, "Manager@123"); 

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Manager");
                    }
                }
            }
        }
    }

}
