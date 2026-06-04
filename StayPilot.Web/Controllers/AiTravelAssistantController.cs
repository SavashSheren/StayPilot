using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.AiDtos;
using StayPilot.Web.Services;
using StayPilot.Web.ViewModels;

namespace StayPilot.Web.Controllers
{
    public class AiTravelAssistantController : Controller
    {
        private readonly IAiTravelAssistantApiService _aiTravelAssistantApiService;

        public AiTravelAssistantController(IAiTravelAssistantApiService aiTravelAssistantApiService)
        {
            _aiTravelAssistantApiService = aiTravelAssistantApiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new AiAssistantPageViewModel
            {
                QuestionForm = new CreateAiTravelQuestionDto
                {
                    Destination = "Istanbul",
                    TravelDate = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd"),
                    TravelStyle = "Culture and comfort",
                    Question = "Where should I stay for a 4-day trip and what should I consider before booking?"
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ask(CreateAiTravelQuestionDto questionForm)
        {
            var model = new AiAssistantPageViewModel
            {
                QuestionForm = questionForm
            };

            if (string.IsNullOrWhiteSpace(questionForm.Question))
            {
                model.ErrorMessage = "Please write a travel question.";

                return View("Index", model);
            }

            var answer = await _aiTravelAssistantApiService.AskAsync(questionForm);

            if (answer is null || !answer.IsSuccessful)
            {
                model.ErrorMessage = answer?.ErrorMessage ?? "AI assistant could not generate an answer.";

                return View("Index", model);
            }

            model.Answer = answer;
            model.HasAnswer = true;

            return View("Index", model);
        }
    }
}