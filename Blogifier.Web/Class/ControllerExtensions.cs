using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Models.AccountViewModels;
using Blogifier.Models.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Web.Class
{
    public static class ControllerExtensions
    {
        public static UsersViewModel GetUsersModel(this Controller controller, IUnitOfWork _db)
        {
            var profile = controller.GetProfile(_db);

            var model = new UsersViewModel
            {
                Profile = profile,
                RegisterModel = new RegisterViewModel()
            };
            model.RegisterModel.SendGridApiKey = _db.CustomFields.GetValue(
                CustomType.Application, profile.Id, Constants.SendGridApiKey);

            return model;
        }

        public static Profile GetProfile(this Controller controller, IUnitOfWork _db)
        {
            return _db.Profiles.Single(b => b.IdentityName == controller.User.Identity.Name);
        }
    }
}
