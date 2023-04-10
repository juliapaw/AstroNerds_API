using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AstroNerds_API.Services;
using System.Text.Json.Serialization;

namespace AstroNerds_API.Entities
{
    public class Horoscope
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ZodiacName { get; set; }
        public string Date_range { get; set; }
        public DateTime Current_date { get; set; }
        public string Description { get; set; }
        public string Compatibility { get; set; }
        public string Mood { get; set; }    
        public byte Lucky_Number { get; set; }
    }
}
