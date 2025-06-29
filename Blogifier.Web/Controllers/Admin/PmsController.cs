using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Web.Controllers.Admin
{
    [Authorize]
    [Route("admin/[controller]")]
    public class PmsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}