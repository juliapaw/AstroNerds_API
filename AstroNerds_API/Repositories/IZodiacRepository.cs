using AstroNerds_API.Entities;
using AstroNerds_API.Models;

namespace AstroNerds_API.Repositories
{
    public interface IZodiacRepository
    {
        Task<IEnumerable<string>> GetZodiacsAsync();
        Task<ZodiacDescriptionDto> GetZodiacByNameAsync(string name);
        public Task<List<ZodiacDescriptionDto>> GetZodiacsByNameToUpdateDescriptionAsync(string name);
        Task<Zodiac> GetZodiacByDateOfBirthAsync(DateTime dateOfBirth);
        public void UpdateZodiacDescriptions(string zodiacName, string newDescription);

        Task SaveChangesAsync();
    }
}
