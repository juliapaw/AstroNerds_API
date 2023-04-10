namespace AstroNerds_API.Models
{
    public class HoroscopeDto
    {
        public string Date_range { get; set; }
        public DateTime Current_date { get; set; }
        public string Description { get; set; }
        public string Compatibility { get; set; }
        public string Mood { get; set; }
        public byte Lucky_Number { get; set; }
        public string ZodiacName { get; set; }
    }
}
