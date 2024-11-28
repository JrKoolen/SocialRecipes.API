﻿using Microsoft.AspNetCore.Mvc;
using SocialRecipes.DAL;

namespace SocialRecipes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public StatusController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

        /// <summary>
        /// Checks if the database connection is alive.
        /// </summary>
        /// <returns>
        /// A success message if the database is reachable, or an error message if not.
        /// </returns>
        [HttpGet("database")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDatabaseStatus()
        {
            try
            {
                // Check database connectivity (e.g., by running a lightweight query)
                await _dbContext.Database.CanConnectAsync();

                // If successful, return OK
                return Ok(new { status = "Database is running", timestamp = DateTime.UtcNow });
            }
            catch (Exception ex)
            {
                // If there's an error, return a 500 response
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "Database connection failed",
                    error = ex.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
