using backEnd.Src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backEnd.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategory category) : ControllerBase
    {
        private readonly ICategory _category = category;

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
