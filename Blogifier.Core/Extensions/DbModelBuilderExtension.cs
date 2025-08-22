using System;
using System.Collections.Generic;
using System.Text;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Extensions
{
    public static class DbModelBuilderExtension
    {
        public static void CreateAspNetModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().ToTable("AppUsers");
            modelBuilder.Entity<IdentityRole<string>>().ToTable("AppRoles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AppUserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AppUserTokens"); 
        }
    }
}
