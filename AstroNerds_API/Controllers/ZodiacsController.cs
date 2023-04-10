using AstroNerds_API.Entities;
using AstroNerds_API.Models;
using AstroNerds_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AstroNerds_API.Controllers
{
    [Route("api/zodiacs")]
    [ApiController]
    public class ZodiacsController : ControllerBase
    {
        private readonly IZodiacRepository _zodiacRepository;
        private readonly ILogger<ZodiacsController> _logger;

        public ZodiacsController(IZodiacRepository zodiacRepository, ILogger<ZodiacsController> logger)
        {
            _zodiacRepository = zodiacRepository;
            _logger = logger;

        }
        /// <summary>
        /// Retrieves all the possible zodiac signs.
        /// </summary>
        /// <returns>A list of strings representing the names of the zodiac signs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetZodiacsAsync()
        {
            try
            {
                var zodiac = await _zodiacRepository.GetZodiacsAsync();
                _logger.LogInformation("Successfully retrieved the list of zodiac");
                return Ok(zodiac);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Zodiac by name: {ErrorMessage}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the Zodiac by name: {ex.Message}");
            }

        }
        /// <summary>
        /// Retrieves zodiac by its name.
        /// </summary>
        /// <param name="name">The name of the zodiac sign to retrieve.</param>
        /// <returns>An ActionResult containing the Zodiac object if it exists, or NotFound if it doesn't.</returns>
        [HttpGet("name")]
        public async Task<ActionResult<ZodiacDescriptionDto>> GetZodiacByNameAsync(string zodiacName)
        {
            try
            {
                var zodiac = await _zodiacRepository.GetZodiacByNameAsync(zodiacName);

                if (zodiac == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Successfully retrieved the Zodiac by name: {ZodiacName}", zodiacName);
                return Ok(zodiac);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving the Zodiac by name: {ErrorMessage}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the Zodiac by name: {ex.Message}");
            }
        }
        /// <summary>
        /// Retrieves the Zodiac sign for a person based on their date of birth.
        /// </summary>
        /// <param name="dateOfBirth">The person's date of birth.</param>
        /// <returns>An ActionResult containing the Zodiac objet or a NotFound response.</returns>
        [HttpGet("dateOfBirth")]
        public async Task<ActionResult<ZodiacDto>> GetZodiacByDateOfBirthAsync(DateTime dateOfBirth)
        {
            try
            {
                var zodiac = await _zodiacRepository.GetZodiacByDateOfBirthAsync(dateOfBirth);

                if (zodiac == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Successfully retrieved the Zodiac for a person based on date of birth." +
                    " Date of birth: {dateOfBirth}", dateOfBirth);
                return Ok(zodiac);
        }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the Zodiac sign: {ex.Message}");
    }
}
        /// <summary>
        /// Updates  the description of the zodiacs with the specified name.
        /// </summary>
        /// <param name="zodiacName">The name of zodiac to update.</param>
        /// <param name="newDescription">The new description to set for the zodiacs</param>
        /// <returns>An ActionResult representing the result of the operation.</returns>
        [HttpPatch("{zodiacName}/UpdateDescription")]
        public async Task<ActionResult> UpdateZodiacDescriptionAsync(string zodiacName, [FromBody] string newDescription)
        {
            try
            {
                var zodiacs = await _zodiacRepository.GetZodiacsByNameToUpdateDescriptionAsync(zodiacName);
                if (zodiacs == null || !zodiacs.Any())
                {
                    return NotFound();
                }

                foreach (var zodiac in zodiacs)
                {
                    _zodiacRepository.UpdateZodiacDescriptions(zodiacName, newDescription);
                }
                _logger.LogInformation($"Successfully updated the description for Zodiac {zodiacName}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the description for Zodiac {zodiacName}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating the Zodiac description: {ex.Message}");
            }
        }
    }
}


    
