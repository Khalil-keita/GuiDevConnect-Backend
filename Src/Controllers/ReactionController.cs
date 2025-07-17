using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactionController(Reaction like) : ControllerBase
    {
        private readonly Reaction _like = like;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
