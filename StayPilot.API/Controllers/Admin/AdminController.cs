using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.HeroDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class HeroSectionsController : ControllerBase
    {
        private readonly IHeroSectionService _heroSectionService;

        public HeroSectionsController(IHeroSectionService heroSectionService)
        {
            _heroSectionService = heroSectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHeroSections()
        {
            var values = await _heroSectionService.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveHeroSections()
        {
            var values = await _heroSectionService.GetActiveListAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHeroSectionById(int id)
        {
            var value = await _heroSectionService.GetByIdAsync(id);

            if (value is null)
            {
                return NotFound(new
                {
                    message = "Hero section not found."
                });
            }

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHeroSection(CreateHeroSectionDto createHeroSectionDto)
        {
            await _heroSectionService.CreateAsync(createHeroSectionDto);

            return Ok(new
            {
                message = "Hero section created successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHeroSection(UpdateHeroSectionDto updateHeroSectionDto)
        {
            var existingHeroSection = await _heroSectionService.GetByIdAsync(updateHeroSectionDto.Id);

            if (existingHeroSection is null)
            {
                return NotFound(new
                {
                    message = "Hero section not found."
                });
            }

            await _heroSectionService.UpdateAsync(updateHeroSectionDto);

            return Ok(new
            {
                message = "Hero section updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeroSection(int id)
        {
            var existingHeroSection = await _heroSectionService.GetByIdAsync(id);

            if (existingHeroSection is null)
            {
                return NotFound(new
                {
                    message = "Hero section not found."
                });
            }

            await _heroSectionService.DeleteAsync(id);

            return Ok(new
            {
                message = "Hero section deleted successfully."
            });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> SetHeroSectionStatus(int id, bool isActive)
        {
            var existingHeroSection = await _heroSectionService.GetByIdAsync(id);

            if (existingHeroSection is null)
            {
                return NotFound(new
                {
                    message = "Hero section not found."
                });
            }

            await _heroSectionService.SetActiveStatusAsync(id, isActive);

            return Ok(new
            {
                message = isActive
                    ? "Hero section activated successfully."
                    : "Hero section deactivated successfully."
            });
        }
    }
}