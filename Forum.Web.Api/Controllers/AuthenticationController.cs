using Forum.Application.Dto;
using Forum.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Forum.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(
            [FromBody] AuthenticateDto model)
        {
            var result = await _authenticationService.AuthenticateAsync(
                model.Username, 
                model.Password);

            if (!string.IsNullOrEmpty(result.Error))
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.User);
        }
    }
}
