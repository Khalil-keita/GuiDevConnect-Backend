using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController(IPost post) : ControllerBase
    {
        private readonly IPost _post = post;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
