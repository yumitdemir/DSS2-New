using Forum.Application.Dto;
using Forum.Application.Services;
using Forum.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Forum.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Forum.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly DatabaseContext _context;

        public UsersController(UserService userService, DatabaseContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();


            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(
            [Required, FromRoute] long userId)
        {
        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            
            var user = await _context.Users.FindAsync(userId);

            
            return Ok(user);
        }

        [HttpPost("{role}")]
        public async Task<IActionResult> Create(
            [FromBody, Required] CreateUserDto user,
            [FromRoute] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
        
            var userId = await _userService.CreateUserAsync(user, role);
         
        
            return CreatedAtAction(
                actionName: nameof(Get),
                routeValues: new { userId = userId.Id },
                value: userId);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(
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
        public async Task<IActionResult> Delete(
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
