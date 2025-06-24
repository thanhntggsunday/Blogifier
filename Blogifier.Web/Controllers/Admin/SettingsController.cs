using System;
using Blogifier.Core.Common;
using Blogifier.Core.Data.Domain;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Extensions;
using Blogifier.Core.Middleware;
using Blogifier.Core.Services.FileSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Services.Email;
using Blogifier.Models.AccountViewModels;
using Blogifier.Web.Class;
using Microsoft.AspNetCore.Identity;

namespace Blogifier.Core.Controllers
{
    [Authorize]
    [Route("admin/[controller]")]
	public class SettingsController : Controller
	{
        private readonly IUnitOfWork _db;
        private readonly ILogger _logger;
        private readonly string _theme;
        private readonly string _pwdTheme = "~/Views/Account/ChangePassword.cshtml";

        public SettingsController(IUnitOfWork db, ILogger<SettingsController> logger)
		{
			_db = db;
            _logger = logger;
            _theme = $"~/{ApplicationSettings.BlogAdminFolder}/Settings/";
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            var model = new SettingsProfile();
            model.Profile = this.GetProfile(_db);
            
            if(model.Profile != null)
            {
                model.AuthorName = model.Profile.AuthorName;
                model.AuthorEmail = model.Profile.AuthorEmail;
                model.Avatar = model.Profile.Avatar;
                model.EmailEnabled = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.SendGridApiKey).Length > 0;
                model.CustomFields = _db.CustomFields.GetUserFields(model.Profile.Id).Result;
            }
            return View(_theme + "Profile.cshtml", model);
        }

        [HttpPost]
        [Route("profile")]
        public IActionResult Profile(SettingsProfile model)
        {
            var profile = this.GetProfile(_db);
            if (ModelState.IsValid)
            {
                if (profile == null)
                {
                    profile = new Profile();

                    if (_db.Profiles.All().ToList().Count == 0)
                    {
                        profile.IsAdmin = true;
                    }
                    profile.AuthorName = model.AuthorName;
                    profile.AuthorEmail = model.AuthorEmail;
                    profile.Avatar = model.Avatar;

                    profile.IdentityName = User.Identity.Name;
                    profile.Slug = SlugFromTitle(profile.AuthorName);
                    profile.Title = BlogSettings.Title;
                    profile.Description = BlogSettings.Description;
                    profile.BlogTheme = BlogSettings.Theme;

                    _db.Profiles.Add(profile);
                }
                else
                {
                    profile.AuthorName = model.AuthorName;
                    profile.AuthorEmail = model.AuthorEmail;
                    profile.Avatar = model.Avatar;
                }
                _db.Complete();

                model.Profile = this.GetProfile(_db);

                // save custom fields
                if(profile.Id > 0 && model.CustomFields != null)
                {
                    SaveCustomFields(model.CustomFields, profile.Id);
                }
                model.CustomFields = _db.CustomFields.GetUserFields(model.Profile.Id).Result;

                ViewBag.Message = "Profile updated";
            }
            return View(_theme + "Profile.cshtml", model);
        }

        [VerifyProfile]
        [Route("about")]
        public IActionResult About()
        {
            return View(_theme + "About.cshtml", new AdminBaseModel { Profile = this.GetProfile(_db) });
        }

        [MustBeAdmin]
        [Route("general")]
        public IActionResult General()
        {
            var profile = this.GetProfile(_db);
            var storage = new BlogStorage("");

            var model = new SettingsGeneral
            {
                Profile = profile,
                BlogThemes = BlogSettings.BlogThemes,
                Title = BlogSettings.Title,
                Description = BlogSettings.Description,
                BlogTheme = BlogSettings.Theme,
                Logo = BlogSettings.Logo,
                Avatar = ApplicationSettings.ProfileAvatar,
                Image = BlogSettings.Cover,
                EmailKey = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.SendGridApiKey),
                BlogHead = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.HeadCode),
                BlogFooter = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.FooterCode)
            };
            return View(_theme + "General.cshtml", model);
        }

        [HttpPost]
        [MustBeAdmin]
        [Route("general")]
        public IActionResult General(SettingsGeneral model)
        {
            var storage = new BlogStorage("");
            model.BlogThemes = BlogSettings.BlogThemes;
            model.Profile = this.GetProfile(_db);

            if (ModelState.IsValid)
            {
                BlogSettings.Title = model.Title;
                BlogSettings.Description = model.Description;
                BlogSettings.Logo = model.Logo;
                ApplicationSettings.ProfileAvatar = model.Avatar;
                BlogSettings.Cover = model.Image;
                BlogSettings.Theme = model.BlogTheme;

                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.Title, model.Title);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.Description, model.Description);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ProfileLogo, model.Logo);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ProfileAvatar, model.Avatar);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ProfileImage, model.Image);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.BlogTheme, model.BlogTheme);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.SendGridApiKey, model.EmailKey);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.HeadCode, model.BlogHead);
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.FooterCode, model.BlogFooter);

                model.Profile.BlogTheme = model.BlogTheme;

                _db.Complete();

                ViewBag.Message = "Updated";
            }
            return View(_theme + "General.cshtml", model);
        }

        [MustBeAdmin]
        [Route("posts")]
        public IActionResult Posts()
        {
            var profile = this.GetProfile(_db);

            var model = new SettingsPosts
            {
                Profile = profile,
                PostImage = BlogSettings.Cover,
                PostFooter = _db.CustomFields.GetValue(CustomType.Application, 0, Constants.PostCode),
                ItemsPerPage = BlogSettings.ItemsPerPage
            };
            return View(_theme + "Posts.cshtml", model);
        }

        [HttpPost]
        [MustBeAdmin]
        [Route("posts")]
        public IActionResult Posts(SettingsPosts model)
        {
            model.Profile = this.GetProfile(_db);

            if (ModelState.IsValid)
            {
                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.ItemsPerPage, model.ItemsPerPage.ToString());
                BlogSettings.ItemsPerPage = model.ItemsPerPage;

                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.PostImage, model.PostImage);
                BlogSettings.PostCover = model.PostImage;

                _db.CustomFields.SetCustomField(CustomType.Application, 0, Constants.PostCode, model.PostFooter);

                _db.Complete();

                ViewBag.Message = "Updated";
            }
            return View(_theme + "Posts.cshtml", model);
        }

        [MustBeAdmin]
        [Route("advanced")]
        public IActionResult Advanced()
        {
            var profile = this.GetProfile(_db);

            var model = new SettingsAdvanced
            {
                Profile = profile
            };
            return View(_theme + "Advanced.cshtml", model);
        }
      
        void SaveCustomFields(Dictionary<string, string> fields, int profileId)
        {
            if(fields != null && fields.Count > 0)
            {
                foreach (var field in fields)
                {
                    _db.CustomFields.SetCustomField(CustomType.Profile, profileId, field.Key, field.Value);
                }
            }
        }
      

        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        [MustBeAdmin]
        [Route("users")]
        public IActionResult Users(int page = 1)
        {
            var profile = this.GetProfile(_db);
            var pager = new Pager(page);
            var blogs = _db.Profiles.ProfileList(p => p.Id > 0, pager);

            var model = this.GetUsersModel(_db);
            model.Blogs = blogs;
            model.Pager = pager;

            return View(_theme + "Users.cshtml", model);
        }
        

        #region Helpers

       
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(BlogController.Index), "Blog");
            }
        }

        string SlugFromTitle(string title)
        {
            var slug = title.ToSlug();
            if (_db.Profiles.Single(b => b.Slug == slug) != null)
            {
                for (int i = 2; i < 100; i++)
                {
                    if (_db.Profiles.Single(b => b.Slug == slug + i.ToString()) == null)
                    {
                        return slug + i.ToString();
                    }
                }
            }
            return slug;
        }

        #endregion
    }
}
