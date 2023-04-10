using System.ComponentModel.DataAnnotations;

namespace AstroNerds_API.Entities
{
    public class DailyHoroscopeFileContent
    {
        [Key]
        public string ZodiacName { get; set; }
        public byte[] PdfContent { get; set; }

        public DailyHoroscopeFileContent(string zodiacName, byte[] pdfContent) 
        {
            ZodiacName = zodiacName;
            PdfContent = pdfContent;
        }
    }
}
