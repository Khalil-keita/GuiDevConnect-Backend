using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController(INotification notification) : ControllerBase
    {
        private readonly INotification _notification = notification;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
