using Microsoft.AspNetCore.Mvc;
using StayPilot.Application.DTOs.DestinationDtos;
using StayPilot.Application.Interfaces;

namespace StayPilot.API.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationService _destinationService;

        public DestinationsController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDestinations()
        {
            var values = await _destinationService.GetAllAsync();

            return Ok(values);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveDestinations()
        {
            var values = await _destinationService.GetActiveListAsync();

            return Ok(values);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedDestinations()
        {
            var values = await _destinationService.GetFeaturedListAsync();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDestinationById(int id)
        {
            var value = await _destinationService.GetByIdAsync(id);

            if (value is null)
            {
                return NotFound(new
                {
                    message = "Destination not found."
                });
            }

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDestination(CreateDestinationDto createDestinationDto)
        {
            await _destinationService.CreateAsync(createDestinationDto);

            return Ok(new
            {
                message = "Destination created successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDestination(UpdateDestinationDto updateDestinationDto)
        {
            var existingDestination = await _destinationService.GetByIdAsync(updateDestinationDto.Id);

            if (existingDestination is null)
            {
                return NotFound(new
                {
                    message = "Destination not found."
                });
            }

            await _destinationService.UpdateAsync(updateDestinationDto);

            return Ok(new
            {
                message = "Destination updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var existingDestination = await _destinationService.GetByIdAsync(id);

            if (existingDestination is null)
            {
                return NotFound(new
                {
                    message = "Destination not found."
                });
            }

            await _destinationService.DeleteAsync(id);

            return Ok(new
            {
                message = "Destination deleted successfully."
            });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> SetDestinationStatus(int id, bool isActive)
        {
            var existingDestination = await _destinationService.GetByIdAsync(id);

            if (existingDestination is null)
            {
                return NotFound(new
                {
                    message = "Destination not found."
                });
            }

            await _destinationService.SetActiveStatusAsync(id, isActive);

            return Ok(new
            {
                message = isActive
                    ? "Destination activated successfully."
                    : "Destination deactivated successfully."
            });
        }
    }
}