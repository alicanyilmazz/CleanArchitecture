using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ProductController");
        }
    }
}
