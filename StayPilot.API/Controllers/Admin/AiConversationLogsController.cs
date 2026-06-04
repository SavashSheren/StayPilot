using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class AiConversationLogsController : ControllerBase
    {
        private readonly IAiConversationLogService _aiConversationLogService;

        public AiConversationLogsController(IAiConversationLogService aiConversationLogService)
        {
            _aiConversationLogService = aiConversationLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAiConversationLogs()
        {
            var values = await _aiConversationLogService.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAiConversationLogById(int id)
        {
            var value = await _aiConversationLogService.GetByIdAsync(id);

            if (value is null)
            {
                return NotFound(new
                {
                    message = "AI conversation log not found."
                });
            }

            return Ok(value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAiConversationLog(int id)
        {
            var value = await _aiConversationLogService.GetByIdAsync(id);

            if (value is null)
            {
                return NotFound(new
                {
                    message = "AI conversation log not found."
                });
            }

            await _aiConversationLogService.DeleteAsync(id);

            return Ok(new
            {
                message = "AI conversation log deleted successfully."
            });
        }
    }
}