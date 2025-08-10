using Blogifier.Admin;
using Blogifier.Core.Data;
using Blogifier.Core.Extensions;
using Blogifier.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Blogifier
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetRequiredService<AppDbContext>();

				try
				{
					if (dbContext.Database.GetPendingMigrations().Any())
						dbContext.Database.Migrate();
                }
                catch { }
			}

            // Seed database
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();

                if (!db.Users.Any())
                {
                    var admin = new User
                    {
                        Email = "admin@example.com",
                        PasswordHash = "123456".Hash(Startup.Salt)
                    };
                    var user = new User
                    {
                        Email = "user@example.com",
                        PasswordHash = "123456".Hash(Startup.Salt)
                    };

                    var roleAdmin = new Role { Name = "Admin" };
                    var roleUser = new Role { Name = "User" };

                    db.Users.AddRange(admin, user);
                    db.Roles.AddRange(roleAdmin, roleUser);
                    db.SaveChanges();

                    db.UserRoles.AddRange(
                        new UserRole { UserId = admin.Id, RoleId = roleAdmin.Id },
                        new UserRole { UserId = user.Id, RoleId = roleUser.Id }
                    );

                    db.SaveChanges();
                }
            }

            host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			 Host.CreateDefaultBuilder(args)
				  .ConfigureWebHostDefaults(webBuilder =>
				  {
					  webBuilder
					  .UseContentRoot(Directory.GetCurrentDirectory())
					  .UseIISIntegration()
					  .UseStartup<Startup>();
				  });
	}
}
