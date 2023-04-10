using AstroNerds_API.Entities;
using AstroNerds_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;

namespace AstroNerds_API.Controllers
{
    [Route("api/horoscopefile")]
    [ApiController]
    public class DailyHoroscopeFileContentController : ControllerBase
    {
        private readonly IDailyHoroscopeFileContentRepository _dailyHoroscopeFileContentRepository;
        private readonly ILogger <DailyHoroscopeFileContent> _logger;

        public DailyHoroscopeFileContentController(IDailyHoroscopeFileContentRepository dailyHoroscopeFileContentRepository,
                                                   ILogger <DailyHoroscopeFileContent> logger)
        {
            _dailyHoroscopeFileContentRepository = dailyHoroscopeFileContentRepository;
            _logger = logger;

        }
        /// <summary>
        /// Returns the daily horoscope for the given zodiac sign in PDF format.
        /// </summary>
        /// <param name="zodiacName">The name of the zodiac sign to get the daily horoscope for.</param>
        /// <returns>An IActionResult containing the daily horoscope for the given zodiac sign in PDF format.</returns>
        [HttpGet("{zodiacName}")]
        [Produces("application/pdf")]
        public IActionResult GetDailyHoroscopePdf(string zodiacName)
        {
            try
            {
                var pdfContent = _dailyHoroscopeFileContentRepository.GetDailyHoroscopePdfContent(zodiacName);

                if (pdfContent == null)
                {
                    return NotFound();
                }

                // Set the Content-Disposition header so that the browser displays the PDF in a window instead of downloading it
                Response.Headers.Add("Content-Disposition", $"inline; filename={zodiacName}");

                return new FileContentResult(pdfContent, "application/pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error generating daily horoscope PDF for zodiac sign {zodiacName}");
                return StatusCode(500, "An error occurred while generating the daily horoscope PDF.");
            }
        }

        /// <summary>
        /// Uploads a file to the server and saves it to the database.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <returns>An IActionResult representing the result of the file upload.</returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("The file was not selected.");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileContent = memoryStream.ToArray();

                    var dailyHoroscopeFileContent = new DailyHoroscopeFileContent(
                        zodiacName: file.FileName,
                        pdfContent: fileContent
                        );

                    _dailyHoroscopeFileContentRepository.Add(dailyHoroscopeFileContent);
                    await _dailyHoroscopeFileContentRepository.SaveChangesAsync();
                }

                return Ok("The file was successfully uploaded and saved to the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while uploading the file.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploading the file.");
            }
        }
    }
}
    
