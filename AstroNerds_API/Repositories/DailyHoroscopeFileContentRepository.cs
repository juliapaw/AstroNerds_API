using AstroNerds_API.DbContexts;
using AstroNerds_API.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AstroNerds_API.Repositories
{
    public class DailyHoroscopeFileContentRepository : IDailyHoroscopeFileContentRepository
    {
        private readonly ZodiacContext _context;

        public DailyHoroscopeFileContentRepository(ZodiacContext context)
        {
            _context = context;
        }

        public byte[] GetDailyHoroscopePdfContent(string zodiacName)
        {
            var dailyHoroscope = _context.DailyHoroscopePdfContent.FirstOrDefault(x => x.ZodiacName == zodiacName);
            if (dailyHoroscope != null)
            {
                return dailyHoroscope.PdfContent;
            }
            return null;
        }
        public async Task Add(DailyHoroscopeFileContent dailyHoroscopeFileContent)
        {
            await _context.DailyHoroscopePdfContent.AddAsync(dailyHoroscopeFileContent);
        }

        public async Task SaveChangesAsync()
        {
            _context.SaveChanges();
        }
    }
}


