using Forum.Web.UI.Clients.Users;
using Forum.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Forum.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserClient _userClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IUserClient userClient,
            ILogger<HomeController> logger)
        {
            _userClient = userClient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var result = await _userClient.GetListAsync();

            return View();
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
