using AstroNerds_API.DbContexts;
using AstroNerds_API.Entities;
using AstroNerds_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AstroNerds_API.Repositories
{
    public class ZodiacRepository : IZodiacRepository
    {
        private readonly ZodiacContext _context;

        public ZodiacRepository(ZodiacContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetZodiacsAsync()
        {
            var zodiacs = await _context.Zodiacs
              .Select(z => z.ZodiacName)
              .Distinct()
              .ToListAsync();

            return zodiacs;
        }

        public async Task<Zodiac> GetZodiacByDateOfBirthAsync(DateTime dateOfBirth)
        {
            var zodiac =  await _context.Zodiacs.Where(z => z.Date_start <= dateOfBirth && z.Date_end >= dateOfBirth).FirstOrDefaultAsync();
            return zodiac;
        }
     
        public async Task<ZodiacDescriptionDto> GetZodiacByNameAsync(string name)
        {
            var zodiac = await _context.Zodiacs.FirstOrDefaultAsync(z => z.ZodiacName == name);

            if (zodiac == null)
            {
                return null;
            }

            var zodiacDescription = new ZodiacDescriptionDto
            {
                ZodiacName = zodiac.ZodiacName,
                Description = zodiac.Description
            };

            return zodiacDescription;
        }

        public async Task<List<ZodiacDescriptionDto>> GetZodiacsByNameToUpdateDescriptionAsync(string name)
        {
            var zodiacs = await _context.Zodiacs
                                    .Where(z => z.ZodiacName == name)
                                    .Select(z => new ZodiacDescriptionDto
                                    {
                                        ZodiacName = z.ZodiacName,
                                        Description = z.Description
                                    })
                                    .ToListAsync();

            return zodiacs;
        }
        public void UpdateZodiacDescriptions(string zodiacName, string newDescription)
        {
            var zodiacs = _context.Zodiacs.Where(z => z.ZodiacName == zodiacName);
            foreach (var zodiac in zodiacs)
            {
                zodiac.Description = newDescription;
            }
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            _context.SaveChanges();
        }
    }
}
