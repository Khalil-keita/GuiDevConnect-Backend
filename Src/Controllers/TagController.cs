using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController(ITag tag) : ControllerBase
    {
        private readonly ITag _tag = tag;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
