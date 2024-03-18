using Forum.Application.Dto;
using Forum.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            if (!string.IsNullOrEmpty(users.Error))
            {
                return BadRequest(users.Error);
            }

            if (!users.Users.Any())
            {
                return NoContent();
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAsync(
            [Required, FromRoute] long? userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var user = await _userService.GetUsersAsync(userId);
            if (!string.IsNullOrEmpty(user.Error))
            {
                return NotFound(user.Error);
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromBody, Required] CreateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var userId = await _userService.CreateUserAsync(user);
            if (!string.IsNullOrEmpty(userId.Error))
            {
                return BadRequest(userId.Error);
            }

            return CreatedAtAction(
                actionName: nameof(GetAsync),
                value: userId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(
            [Required, FromRoute] long? userId,
            [FromBody, Required] UpdateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var (_, Error) = await _userService.UpdatedUserAsync(userId, user);
            if (!string.IsNullOrEmpty(Error))
            {
                return BadRequest(Error);
            }

            var userDto = await _userService.GetUsersAsync(userId);
            if (!string.IsNullOrEmpty(userDto.Error))
            {
                return BadRequest(userDto.Error);
            }

            return Ok(userDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(
           [Required, FromRoute] long? userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var user = await _userService.DeleteUserAsync(userId);
            if (!string.IsNullOrEmpty(user.Error))
            {
                return BadRequest(user.Error);
            }

            return Ok(user.User);
        }
    }
}
