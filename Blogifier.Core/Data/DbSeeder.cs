using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using DocumentFormat.OpenXml.VariantTypes;
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
                string[] roleNames = { "Admin", "Manager", "Employee", "World" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

              
                await CreateUser(userManager, "admin@demo.com", "Admin@123", "Admin");
                await CreateUser(userManager, "manager@demo.com", "Manager@123", "Manager");
                await CreateUser(userManager, "employee01@demo.com", "Employee01@123", "Employee");
                await CreateUser(userManager, "guest01@demo.com", "Guest01@123", "World");

              
            }
        }

        private static async Task CreateUser(UserManager<ApplicationUser> userManager, string email, string pass, string roleName)
        {
            var acc = await userManager.FindByEmailAsync(email);

            if (acc == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, pass);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }

                bool isAdmin = roleName.ToUpper() == RoleName.Admin.ToUpper();

                AddProfile(email, isAdmin);
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
