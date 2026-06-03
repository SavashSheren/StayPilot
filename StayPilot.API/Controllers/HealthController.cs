using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayPilot.Infrastructure.Context;

namespace StayPilot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly StayPilotDbContext _context;

        public HealthController(StayPilotDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetHealthStatus()
        {
            return Ok(new
            {
                status = "Healthy",
                project = "StayPilot",
                service = "StayPilot.API",
                message = "StayPilot API is running successfully.",
                checkedAt = DateTime.UtcNow
            });
        }

        [HttpGet("database")]
        public async Task<IActionResult> GetDatabaseHealthStatus()
        {
            var canConnect = await _context.Database.CanConnectAsync();

            return Ok(new
            {
                database = "StayPilotDb",
                canConnect,
                checkedAt = DateTime.UtcNow
            });
        }
    }
}