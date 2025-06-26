using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Common;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class PmsDefaultController : Controller
    {
        public IActionResult Index()
        {
            return View($"~/{ApplicationSettings.BlogThemesFolder}/OneFour/Default.cshtml");
        }
    }
}