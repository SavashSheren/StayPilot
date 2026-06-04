using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.AiDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Travel
{
    [Route("api/travel/[controller]")]
    [ApiController]
    public class AiAssistantController : ControllerBase
    {
        private readonly IAiTravelAssistantService _aiTravelAssistantService;

        public AiAssistantController(IAiTravelAssistantService aiTravelAssistantService)
        {
            _aiTravelAssistantService = aiTravelAssistantService;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> AskTravelAssistant(CreateAiTravelQuestionDto questionDto)
        {
            if (string.IsNullOrWhiteSpace(questionDto.Question))
            {
                return BadRequest(new
                {
                    message = "Question is required."
                });
            }

            var userIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers.UserAgent.ToString();

            var result = await _aiTravelAssistantService.AskAsync(
                questionDto,
                userIpAddress,
                userAgent);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}