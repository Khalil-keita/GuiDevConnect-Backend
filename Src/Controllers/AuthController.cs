using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuth auth) : ControllerBase
    {
        private readonly IAuth _auth = auth;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
