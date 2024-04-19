using Forum.Web.UI.Clients.Authentication;
using Forum.Web.UI.Clients.Users;
using Forum.Web.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;

namespace Forum.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserClient _userClient;
        private readonly IAuthenticationClient _authenticationClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IUserClient userClient,
            IAuthenticationClient authenticationClient,
            ILogger<HomeController> logger)
        {
            _userClient = userClient;
            _authenticationClient = authenticationClient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction(nameof(AuthController.Login), "Auth");
            }
            
            

            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }
    }
}
