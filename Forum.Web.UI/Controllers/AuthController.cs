using Forum.Web.UI.Clients.Authentication;
using Forum.Web.UI.Clients.Users;
using Forum.Web.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Web.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserClient _userClient;
        private readonly IAuthenticationClient _authenticationClient;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IUserClient userClient,
            IAuthenticationClient authenticationClient,
            ILogger<AuthController> logger)
        {
            _userClient = userClient;
            _authenticationClient = authenticationClient;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {

                var user = await _authenticationClient
                    .LoginAsync(new AuthenticateRequest
                {
                    Username = model.Username,
                    Password = model.Password
                });

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, user.Role.ToString()!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ModelState.AddModelError("", "Invalid username or password");
                
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var result = await _userClient.CreateAsync(new CreateUserRequest
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword
                }, "User");

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{result.FirstName} {result.LastName}"),
                    new Claim(ClaimTypes.Email, result.Email!),
                    new Claim(ClaimTypes.Role, result.Role.ToString()!),
                    new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()!),
                    new Claim(ClaimTypes.Sid, result.Id.ToString()),
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ModelState.AddModelError("", "Unable to register user");
        
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}