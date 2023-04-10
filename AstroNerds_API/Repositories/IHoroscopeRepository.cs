using AstroNerds_API.Entities;
using AstroNerds_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AstroNerds_API.Repositories
{
    public interface IHoroscopeRepository
    {
        //ok
        //Task<Horoscope> GetHoroscope(string zodiacName);
        //test
        Task<Horoscope> GetHoroscope(string zodiacName);
        public void DeleteDailyDescriptionForZodiac(DateTime current_date, string zodiacName, string description);   
        public void AddDailyHoroscope(Horoscope horoscope);
    }
}
