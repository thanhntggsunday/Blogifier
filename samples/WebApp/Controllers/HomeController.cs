using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Services.Logger;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IAppLogger<HomeController> _logger;

        public HomeController(IAppLogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogInformation("HomeController constructor");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
