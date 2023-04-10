
using AstroNerds_API.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AstroNerds_API.DbContexts
{
    public class ZodiacContext : DbContext
    {
        public DbSet<Zodiac> Zodiacs { get; set; }
        public DbSet<Horoscope> Horoscope { get; set; }
       public DbSet<DailyHoroscopeFileContent> DailyHoroscopePdfContent { get; set; }
       public SqlConnection GetDbConnection()
       {
            return (SqlConnection)Database.GetDbConnection();
       }

        public ZodiacContext(DbContextOptions<ZodiacContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}