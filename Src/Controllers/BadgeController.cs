using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BadgeController(IBadge badge) : ControllerBase
    {
        private readonly IBadge _badge = badge;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
