using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController(IPoll poll) : ControllerBase
    {
        private readonly IPoll _poll = poll;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
