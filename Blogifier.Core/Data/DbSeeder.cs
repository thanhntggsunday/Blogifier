using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, "Admin@123"); // pass mặc định

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }

                    AddProfile(adminEmail, true);
                }

                var manaEmail = "manager@demo.com";
                var manaUser = await userManager.FindByEmailAsync(manaEmail);

                if (manaUser == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = manaEmail,
                        Email = manaEmail,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, "Manager@123"); 

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Manager");
                    }

                    AddProfile(manaEmail, false);
                }
            }
        }

        private static void AddProfile(string email, bool isAdmin)
        {
            var builder = new DbContextOptionsBuilder<BlogifierDbContext>();

            ApplicationSettings.DatabaseOptions(builder);

            var options = builder.Options;

            using (var context = new BlogifierDbContext(options))
            {
                // create new profile
                var profile = new Profile();
                profile.IsAdmin = isAdmin;

                profile.AuthorName = email;
                profile.AuthorEmail = email;
                profile.Title = "New blog";
                profile.Description = "New blog description";

                profile.IdentityName = email;
                profile.Slug = email;
                profile.Avatar = ApplicationSettings.ProfileAvatar;
                profile.BlogTheme = BlogSettings.Theme;

                profile.LastUpdated = Core.Common.SystemClock.Now();

                context.Profiles.Add(profile);
                context.SaveChanges();
            }
        }
    }

}
