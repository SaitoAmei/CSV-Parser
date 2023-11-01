

using AutoMapper;
using CSV_Parser.Data.Models;
using System.Globalization;

namespace CSV_Parser.Data.Maps
{
    public class EqualMapperProfile:Profile
    {
        public EqualMapperProfile()
        {
            CreateMap<string, DateTime>().ConvertUsing<DateTimeTypeConverter>();

            CreateMap<TaxiHistoryModel, OrdersHistory>()
                .ForMember(dest => dest.TpepPickupDatetime, opt => opt.MapFrom(src => src.TpepPickupDatetime))
                .ForMember(dest => dest.TpepDropoffDatetime, opt => opt.MapFrom(src => src.TpepDropoffDatetime));
        }
    }

    public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo("en-US");
            if (DateTime.TryParseExact(source, "MM/dd/yyyy hh:mm:ss tt", cultureInfo, DateTimeStyles.None, out var result))
            {
                TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

                result = TimeZoneInfo.ConvertTimeToUtc(result, sourceTimeZone);
                return result;
            }
            return default;
        }
    }
}
