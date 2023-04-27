using AstroNerds_API.Entities;
using AstroNerds_API.Models;
using AstroNerds_API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq.Expressions;

namespace AstroNerds_API.Controllers
{
    [Route("api/horoscope")]
    [ApiController]
    public class HoroscopeController : ControllerBase
    {
        private readonly IHoroscopeRepository _horoscopeRepository;
        private readonly ILogger<HoroscopeController> _logger;
        private readonly IMapper _mapper;
        public HoroscopeController(IHoroscopeRepository horoscopeRepository,
                                    IMapper mapper,
                                    ILogger<HoroscopeController> logger)
        {
            _horoscopeRepository = horoscopeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the daily horoscope for the specified zodiac sign.
        /// </summary>
        /// <param name="zodiacName">The name of the zodiac sign for which the horoscope is to be retrieved.</param>
        /// <returns>The daily horoscope for the specified zodiac sign.</returns>
        [HttpGet("zodiacName")]
        public async Task<ActionResult<Horoscope>> GetHoroscope(string zodiacName)
        {
            try
            {
                var horoscope = await _horoscopeRepository.GetHoroscope(zodiacName);

                if (horoscope == null)
                {
                    return NotFound();
                }

                return Ok(horoscope);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, $"Error retrieving horoscope for zodiac sign {zodiacName}");
                return StatusCode(500, "An error occurred while retrieving the horoscope.");
            }
        }

        /// <summary>
        /// Deletes the daily horoscope description for the given zodiac sign,date and description. 
        /// </summary>
        /// <param name="current_date">The current date of the horoscope.</param>
        /// <param name="zodiacName">The current date of the horoscope.</param>
        /// <param name="description">The description of the horoscope.</param>
        /// <returns>An ActionResult indicating the result of the operation.</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteDailyDescriptionForZodiac(DateTime current_date, string zodiacName, string description)
        {
            try
            {
                await Task.Run(() => _horoscopeRepository.DeleteDailyDescriptionForZodiac(current_date, zodiacName, description));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting daily description for zodiac sign {ZodiacName}", zodiacName);
                return StatusCode(500, "An error occurred while deleting the daily description for the zodiac sign.");
            }
        }



        /// <summary>
        ///Adds a daily horoscope for a given zodiac sign with the following information: zodiac sign, date range, current date, description, compatibility with other zodiac signs, mood, and lucky number.
        /// </summary>
        /// <param name="dailyHoroscopeDto">An object of type AddDailyHoroscopeDto that includes information about the daily horoscope.</param>
        /// <returns>An ActionResult representing the status of the operation.</returns>
        [HttpPost]
        public async Task<ActionResult> AddDailyHoroscope([FromBody] AddDailyHoroscopeDto dailyHoroscopeDto)
        {
            try
            {
                await Task.Run(() =>
                {
                    var horoscope = _mapper.Map<Horoscope>(dailyHoroscopeDto);
                    _horoscopeRepository.AddDailyHoroscope(horoscope);
                });

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding daily horoscope");
                return StatusCode(500, "An error occurred while adding the daily horoscope.");
            }
        }
    }
}
