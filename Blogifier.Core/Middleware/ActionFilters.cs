using System.Collections.Generic;
using System.Linq;
using Blogifier.Core.Common;
using Blogifier.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Blogifier.Core.Middleware
{
    /// <summary>
    /// There are two-level authorization for admin panel
    /// 1. [Authorize] - Verify that user is authenticated
    /// 2. [VerifyProfile] - Verify that user has profile
    /// If user does not have profile - any admin actions
    /// redirected to setup page that must be completed
    /// </summary>
    public class VerifyProfile : ActionFilterAttribute
    {
        DbContextOptions<BlogifierDbContext> _options;
        List<string> AminPageRoleAllowAccess = new List<string>()
        {
            RoleName.Admin, RoleName.Manager, RoleName.Employee
        };

        public VerifyProfile()
        {
            var builder = new DbContextOptionsBuilder<BlogifierDbContext>();

            ApplicationSettings.DatabaseOptions(builder);

            _options = builder.Options;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var context = new BlogifierDbContext(_options))
            {
                var user = filterContext.HttpContext.User.Identity.Name;
                var profile = context.GetProfile(user);

                if (profile == null || profile.Roles.Count == 0)
                {
                    filterContext.Result = new RedirectResult("~/Error/403");
                }

                if (!AminPageRoleAllowAccess.Any(r => profile != null && profile.Roles.Any(r2=>r2.Name.ToUpper() == r.ToUpper())))
                {
                    filterContext.Result = new RedirectResult("~/Error/403");
                }
            }
        }
    }

    public class MustBeAdmin : ActionFilterAttribute
    {
        DbContextOptions<BlogifierDbContext> _options;

        public MustBeAdmin()
        {
            var builder = new DbContextOptionsBuilder<BlogifierDbContext>();

            ApplicationSettings.DatabaseOptions(builder);

            _options = builder.Options;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var context = new BlogifierDbContext(_options))
            {
                var loggedUser = filterContext.HttpContext.User.Identity.Name;
                // var profile = context.Profiles.SingleOrDefaultAsync(p => p.IdentityName == loggedUser).Result;
                var profile = context.GetProfile(loggedUser);

                if (profile == null || profile.Roles.All(r => r.Name.ToUpper() != RoleName.Admin.ToUpper()))
                {
                    filterContext.Result = new RedirectResult("~/Error/403");
                }
            }
        }
    }
}