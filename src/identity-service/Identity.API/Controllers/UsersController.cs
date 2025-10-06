using Identity.Application.Contracts;
using Identity.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserCommands _userService;

        public UsersController(IUserCommands userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateUser(
            [FromForm] CreateUserDto request,
            CancellationToken cancellationToken)
        {
            var userId = await _userService.CreateUserAsync(request, cancellationToken);
            return Ok(userId);
        }

    }
}
