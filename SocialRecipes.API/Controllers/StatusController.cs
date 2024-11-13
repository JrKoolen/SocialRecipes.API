using Microsoft.AspNetCore.Mvc;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "Service is running", timestamp = DateTime.UtcNow });
        }
    }
}
