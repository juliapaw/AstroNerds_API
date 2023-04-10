using AstroNerds_API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AstroNerds_API.Repositories
{
    public interface IDailyHoroscopeFileContentRepository
    {
        public byte[] GetDailyHoroscopePdfContent(string zodiacName);
        public Task Add(DailyHoroscopeFileContent dailyHoroscopeFileContent);
        Task SaveChangesAsync();
    }
}
