using Identity.Application.Contracts;
using Identity.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("identity")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        //app.MapGet("/identity/health", () => Results.Ok(new { ok = true, svc = "identity" }));
        [HttpGet("health")]
        public async Task<IActionResult> Health()
        {
            return Ok(new { ok = true, svc = "identity" });
        }

        /*[HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request, cancellationToken);
            if (result == null) return Unauthorized("Invalid credentials");

            return Ok(result);
        }*/

    }
}
