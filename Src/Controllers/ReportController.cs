using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController(IReport report) : ControllerBase
    {
        private readonly IReport _report = report;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
