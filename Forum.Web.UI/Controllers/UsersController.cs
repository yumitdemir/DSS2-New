using Forum.Application.Dto;
using Forum.Web.UI.Clients.Users;
using Forum.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.UI.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class UsersController : Controller
    {
        private readonly IUserClient _userClient;

        public UsersController(IUserClient userClient) 
        {
            _userClient = userClient;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var users = await _userClient.GetListAsync();

            var viewModel = users.Select(e => new UserShortViewModel
            {
                FullName = e.FullName,
                Id = e.Id,
                Username = e.Username
            })
            .ToArray();

            return View(viewModel);
        }

        // GET: UsersContrller/Details/5
        [HttpGet("Details/{id}")]
        public async Task<ActionResult> Details([FromRoute] long id)
        {
            var user = await _userClient.GetAsync(id);
            
            if (user is null)
            {
                ModelState.AddModelError("",
                    $"User with such Id ({id}) does not exists");

                return View(new UserDetailsViewModel());
            }

            var viewModel = new UserDetailsViewModel
            {
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            };

            return View(viewModel);
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] CreateUserViewModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                var result = await _userClient.CreateAsync(new CreateUserRequest
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    ConfirmPassword = user.ConfirmPassword
                }, "User");

                return RedirectToAction(nameof(Details), result.Id);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(user);
            }
        }

        [HttpGet("Edit/{id}")]
        public async Task<ActionResult> Edit([FromRoute] long id)
        {
            UserDetailsResponse user;
            try
            {
                user = await _userClient.GetAsync(id);
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }

            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }


            return View(user);
        }

        // POST: UsersContrller/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [FromRoute] long id, 
            [FromForm] UpdateUserViewModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }

                var result = await _userClient.UpdateAsync(id, new UpdateUserRequest
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username
                });

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: UsersContrller/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            try
            {
                var user = await _userClient.GetAsync(id);
                var viewModel = new UserDetailsViewModel
                {
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(new UserDetailsViewModel());
            }
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(
            [FromRoute] long id, 
            [FromForm] UserDetailsViewModel user)
        {
            try
            {
                var result = await _userClient.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }
    }
}
