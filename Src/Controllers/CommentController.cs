using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController(IComment comment) : ControllerBase
    {
        private readonly IComment _comment = comment;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
