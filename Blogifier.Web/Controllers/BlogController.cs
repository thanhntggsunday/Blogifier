using Blogifier.Core.Common;
using Blogifier.Core.Services.Data;
using Blogifier.Core.Services.Syndication.Rss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Controllers
{
    public class BlogController : Controller
	{
        IRssService _rss;
        IDataService _ds;
        private readonly ILogger _logger;
        private readonly string _theme;

		public BlogController(IRssService rss, IDataService ds, ILogger<BlogController> logger)
		{
            _rss = rss;
            _ds = ds;
            _logger = logger;
            _theme = $"~/{ApplicationSettings.BlogThemesFolder}/{BlogSettings.Theme}/";

            Logger.LogInformation($" Client Theme: {_theme}");
            ////
            //var productProvider = new ProductProvider();
            //var items = productProvider.GetProducts();
            //Console.WriteLine(items);

            ////
            //productProvider = new ProductProvider();

            //var product = new ProductDto
            //{
            //    Name = "Laptop X500--",
            //    Slug = "laptop-x500",
            //    Summary = "Gaming laptop",
            //    Description = "High performance laptop for gaming",
            //    ImageFile = "x500.jpg",
            //    UnitPrice = 1599.99m,
            //    UnitsInStock = 15,
            //    Star = 4.8,
            //    CategoryId = 2,
            //    CreatedBy = "admin",
            //    ModifiedBy = "admin",
            //    CreatedDate = DateTime.Now,
            //    ModifiedDate = DateTime.Now
            //};

            //productProvider.CreatProducts(product);
        }

        public IActionResult Index(int page = 1)
        {
            var defaultPage = HttpContext.Session.GetString("default_page");

            if (string.IsNullOrEmpty(defaultPage))
            {
                HttpContext.Session.SetString("default_page", "/pmshome");

                return RedirectToAction("Index", "PmsDefault");
            }

            var model = _ds.GetPosts(page);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Index.cshtml", model);
        }

        [Route("{slug:author}")]
        public IActionResult PostsByAuthor(string slug, int page = 1)
        {
            var model = _ds.GetPostsByAuthor(slug, page);
            if(model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + model.Profile.BlogTheme + "/Author.cshtml", model);
        }

        [Route("category/{cat}")]
        public IActionResult AllPostsByCategory(string cat, int page = 1)
        {
            var model = _ds.GetAllPostsByCategory(cat, page);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/{BlogSettings.Theme}/Category.cshtml", model);
        }

        [Route("{slug:author}/{cat}")]
        public IActionResult PostsByCategory(string slug, string cat, int page = 1)
        {
            var model = _ds.GetPostsByCategory(slug, cat, page);
            if(model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + model.Profile.BlogTheme + "/Category.cshtml", model);
        }

        [Route("{slug}")]
        public IActionResult SinglePublication(string slug)
        {
            var model = _ds.GetPostBySlug(slug);
            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View($"~/{ApplicationSettings.BlogThemesFolder}/" + model.Profile.BlogTheme + "/Single.cshtml", model);
        }

        [Route("search/{term}")]
        public IActionResult PagedSearch(string term, int page = 1)
        {
            ViewBag.Term = term;
            var model = _ds.SearchPosts(term, page);

            if (model == null)
                return View(_theme + "Error.cshtml", 404);

            return View(_theme + "Search.cshtml", model);
        }

        [HttpPost]
        public IActionResult Search()
        {
            ViewBag.Term = HttpContext.Request.Form["term"];
            var model = _ds.SearchPosts(ViewBag.Term, 1);

            return View(_theme + "Search.cshtml", model);
        }

        [Route("rss/{slug:author?}")]
        public IActionResult Rss(string slug)
        {
            var absoluteUri = string.Concat(
                Request.Scheme, "://",
                Request.Host.ToUriComponent(),
                Request.PathBase.ToUriComponent());

            var x = slug;

            var rss = _rss.Display(absoluteUri, slug);
            return Content(rss, "text/xml");
        }

        [Route("error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            return View(_theme + "Error.cshtml", statusCode);
        }

    }
}