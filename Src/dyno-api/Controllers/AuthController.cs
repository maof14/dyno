using Common;
using dyno_api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dyno_api.Controllers
{
    [AllowAnonymous]
    [Route($"api/{Routes.Auth}")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApiConfiguration _apiConfiguration;
        private readonly IAuthHelpers _authHelpers;

        public AuthController(IConfiguration configuration, ApiConfiguration apiConfiguration, IAuthHelpers authHelpers)
        {
            _configuration = configuration;
            _apiConfiguration = apiConfiguration;
            _authHelpers = authHelpers;
        }

        [HttpPost]
        public ActionResult Login([FromBody] LoginModel loginModel)
        {
            // Do something about this
            if(loginModel.UserName == "user" && loginModel.Password == "password")
            {
                var token = _authHelpers.GenerateJwtToken();
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
