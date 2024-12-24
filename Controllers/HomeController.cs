using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web_siteResume.BL.Auth;
using Web_siteResume.Models;

namespace Web_siteResume.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUser currentUser;

        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser)
        {            
            this.currentUser = currentUser;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(currentUser.IsLoggedIn());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
