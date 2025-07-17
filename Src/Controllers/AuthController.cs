using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuth auth) : ControllerBase
    {
        private readonly IAuth _auth = auth;

        [HttpPost("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }

        [HttpPost("refresh-token")]
        public IActionResult Refresh()
        {
            return Ok();
        }
    }
}
