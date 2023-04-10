using AstroNerds_API.DbContexts;
using AstroNerds_API.Entities;
using AstroNerds_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AstroNerds_API.Repositories
{
    public class HoroscopeRepository : IHoroscopeRepository
    {
        private readonly ZodiacContext _context;

        public HoroscopeRepository(ZodiacContext context)
        {
            _context = context;
        }
        public async Task<Horoscope> GetHoroscope(string zodiacName)
        {
            // setting a sample date
            var currentDate = new DateTime(2023,07,03);
            
            // if the current date is available in the database
            // var currentDate = DateTime.Today;

            var horoscope = _context.Horoscope.FirstOrDefault(z => z.ZodiacName == zodiacName && z.Current_date == currentDate);

            if (horoscope == null)
            {
                return null;
            }

            horoscope.Current_date = currentDate;
            return horoscope;
        }

        public void DeleteDailyDescriptionForZodiac(DateTime current_date, string zodiacName, string description)
        {
            var horoscopes = _context.Horoscope.Where(
                    z => z.ZodiacName == zodiacName &&
                    z.Current_date == current_date &&
                    z.Description == description);

            foreach (var horoscope in horoscopes)
            {
                _context.Horoscope.RemoveRange(horoscope);
            }
           
            _context.SaveChanges();        
        }

        public void AddDailyHoroscope(Horoscope horoscope)
        {
            _context.Horoscope.Add(horoscope);
            _context.SaveChanges();
        }
    }
}
