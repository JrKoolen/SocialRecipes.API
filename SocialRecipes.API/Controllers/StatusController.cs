using Microsoft.AspNetCore.Mvc;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        /// <summary>
        /// Retrieves the current status of the API.
        /// </summary>
        /// <returns>
        /// A success message with the service status and a UTC timestamp indicating the current time.
        /// </returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "Service is running", timestamp = DateTime.UtcNow });
        }
    }
}
