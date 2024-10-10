using Common;
using dyno_api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dyno_api.Controllers;

[AllowAnonymous]
[Route($"api/{Routes.Auth}")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ApiConfiguration _apiConfiguration;
    private readonly IAuthHelpers _authHelpers;
    private readonly IUserService _userService;

    public AuthController(IConfiguration configuration, ApiConfiguration apiConfiguration, IAuthHelpers authHelpers, IUserService userService)
    {
        _configuration = configuration;
        _apiConfiguration = apiConfiguration;
        _authHelpers = authHelpers;
        _userService = userService;
    }

    [HttpPost(Routes.Login)]
    public async Task<ActionResult> Login([FromBody] LoginModel login)
    {
        var userId = await _userService.Authenticate(login.Username, login.Password);

        if(userId != Guid.Empty)
        {
            var token = _authHelpers.GenerateJwtToken(userId, login.Username);
            return Ok(new { token });
        }

        return Unauthorized();
    }

    [HttpGet(Routes.RegisteringAvailable)]
    public ActionResult RegisteringAvailable() => Ok(_apiConfiguration.RegisteringAvailable);

    [HttpPost(Routes.Register)]
    public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
    {
        if (!_apiConfiguration.RegisteringAvailable)
            return Problem();

        if (!registerModel.Password.Equals(registerModel.PasswordRepeat, StringComparison.Ordinal))
            return BadRequest("Passwords don't match.");

        var result = await _userService.CreateUser(registerModel.Username, registerModel.Username);

        if (result)
            return Ok();

        return Problem();
    }
}
