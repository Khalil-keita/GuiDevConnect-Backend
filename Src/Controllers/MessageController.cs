using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController(IMessage message) : ControllerBase
    {
        private readonly IMessage _message = message;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
