using Microsoft.AspNetCore.Mvc;

namespace StayPilot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
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
    }
}