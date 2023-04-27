using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PBugTracker.Models;
using System.Diagnostics;

namespace PBugTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}