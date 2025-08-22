using System;
using Blogifier.Core;
using Blogifier.Core.Common;
using Blogifier.Core.Data;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.Log;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Blogifier
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            System.Action<DbContextOptionsBuilder> databaseOptions = options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            services.AddDbContext<BlogifierDbContext>(databaseOptions);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BlogifierDbContext>()
                .AddDefaultTokenProviders();

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddDistributedMemoryCache(); 
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                // options.Cookie.IsEssential = true;
            });

            services.AddMvc()
            .ConfigureApplicationPartManager(p =>
            {
                foreach (var assembly in ApplicationSettings.GetAssemblies())
                {
                    if (assembly.GetName().Name != "Blogifier.Web" && assembly.GetName().Name != "Blogifier.Core")
                    {
                        p.ApplicationParts.Add(new AssemblyPart(assembly));
                    }
                }
            });

            services.AddBlogifier(databaseOptions, Configuration);
            services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            // app.UseETagger();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Blog}/{action=Index}/{id?}");
            });

            app.UseBlogifier(env);

            // 🔑 Seed với scope
            using (var scope = app.ApplicationServices.CreateScope())
            {
                try
                {
                    var services = scope.ServiceProvider;
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    DbSeeder.SeedDataAsync(services, userManager, roleManager).Wait();
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
               
            }

        }
    }
}
