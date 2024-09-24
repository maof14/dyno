using Common;
using Microsoft.AspNetCore.Mvc;

namespace dyno_api.Controllers
{
    [Route($"api/{Routes.Ping}")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet(Routes.AreYouAlive)]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
