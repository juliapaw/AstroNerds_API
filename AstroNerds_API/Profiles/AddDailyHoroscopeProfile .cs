using AstroNerds_API.Entities;
using AstroNerds_API.Models;
using AutoMapper;
using System.Globalization;

namespace AstroNerds_API.Profiles
{
    public class AddDailyHoroscopeProfile : Profile
    {
        public AddDailyHoroscopeProfile()
        {
            CreateMap<AddDailyHoroscopeDto, Horoscope>()
                .ForMember(dest => dest.Date_range, opt => opt.MapFrom(src => src.Date_range))
                .ForMember(dest => dest.Current_date, opt => opt.MapFrom(src => DateTime.ParseExact(src.Current_date, "yyyy-MM-dd", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Compatibility, opt => opt.MapFrom(src => src.Compatibility))
                .ForMember(dest => dest.Mood, opt => opt.MapFrom(src => src.Mood))
                .ForMember(dest => dest.Lucky_Number, opt => opt.MapFrom(src => src.Lucky_Number))
                .ForMember(dest => dest.ZodiacName, opt => opt.MapFrom(src => src.ZodiacName));
        }
    }
}
