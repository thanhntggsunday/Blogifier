using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blogifier.Core.Data.Domain;
using Microsoft.AspNetCore.Identity;

namespace Blogifier.Core.Data
{
    public static class DataExtensions
    {
        public static Profile GetProfile(this BlogifierDbContext db, string email)
        {
            var result = db.Profiles.Single(b => b.IdentityName.ToLower() == email.ToLower());

            if (result == null)
            {
                return null;
            }

            var roles = db.GetRolesByEmail(email);
            result.Roles = roles;

            return result;
        }

        public static List<IdentityRole> GetRolesByEmail(this BlogifierDbContext context, string email)
        {
            var roles = (from u in context.Users
                where u.Email.ToLower() == email.ToLower()
                join ur in context.UserRoles on u.Id equals ur.UserId
                join r in context.Roles on ur.RoleId equals r.Id
                select r).ToList();

            return roles;
        }

    }
}
