using System.ComponentModel.DataAnnotations;

namespace AstroNerds_API.Models
{
    public class DailyHoroscopeFileContentDto
    {
        private readonly byte[] _pdfContent;

        [Key]
        public string ZodiacName { get; set; }
        public byte[] PdfContent { get { return _pdfContent; } }

        public DailyHoroscopeFileContentDto() { }
        public DailyHoroscopeFileContentDto(string zodiacName, byte[] pdfContent)
        {
            ZodiacName = zodiacName;
            _pdfContent = pdfContent;
        }
    }
}
