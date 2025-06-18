using Blogifier.Core.Blogs;
using Blogifier.Core.Posts;
using Blogifier.Models;
using Blogifier.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Blogifier.Controllers;

public class HomeController(
  ILogger<HomeController> logger,
  MainMamager mainMamager,
  PostProvider postProvider) : Controller
{
  private readonly ILogger _logger = logger;
  private readonly MainMamager _mainMamager = mainMamager;
  private readonly PostProvider _postProvider = postProvider;

  [HttpGet]
  public async Task<IActionResult> Index([FromQuery] int page = 1)
  {
    MainDto main;
    try
    {
      main = await _mainMamager.GetAsync();
    }
    catch (BlogNotIitializeException ex)
    {
      _logger.LogError(ex, "blgo not iitialize redirect");
      return Redirect("~/account/initialize");
    }
    var pager = await _postProvider.GetPostsAsync(page, main.ItemsPerPage);
    pager.Configure(main.PathUrl, "page");
    var model = new IndexModel(pager, main);
    return View($"~/Views/Themes/{main.Theme}/index.cshtml", model);
  }
}
