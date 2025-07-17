using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThreadController(IThread thread) : ControllerBase
    {
        private readonly IThread _thread = thread;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
