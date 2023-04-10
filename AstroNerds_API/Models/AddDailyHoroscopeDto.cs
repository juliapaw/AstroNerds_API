using AstroNerds_API.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AstroNerds_API.Models
{
    public class AddDailyHoroscopeDto
    {
        public string Date_range { get; set; }
        public string Current_date { get; set; }
        public string Description { get; set; }
        public string Compatibility { get; set; }
        public string Mood { get; set; }
        public byte Lucky_Number { get; set; }
        public string ZodiacName { get; set; }
    }
}

