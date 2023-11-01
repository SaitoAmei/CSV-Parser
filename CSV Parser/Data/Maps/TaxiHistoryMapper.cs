using CSV_Parser.Data.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CSV_Parser.Data.Maps
{
    public class TaxiHistoryMapper : ClassMap<TaxiHistoryModel>
    {
        public TaxiHistoryMapper()
        {

            Map(m => m.TpepPickupDatetime).Name("tpep_pickup_datetime");
            Map(m => m.TpepDropoffDatetime).Name("tpep_dropoff_datetime");
            Map(m => m.PassengerCount).Name("passenger_count").TypeConverter(new CustomTypeConverter<int>());
            Map(m => m.TripDistance).Name("trip_distance");
            Map(m => m.StoreAndFwdFlag).Name("store_and_fwd_flag").TypeConverter(new CustomTypeConverter<string>());
            Map(m => m.PULocationID).Name("PULocationID").TypeConverter(new CustomTypeConverter<int>());
            Map(m => m.DOLocationID).Name("DOLocationID").TypeConverter(new CustomTypeConverter<int>());
            Map(m => m.FareAmount).Name("fare_amount");
            Map(m => m.TipAmount).Name("tip_amount");
        }
    }

    public class CustomTypeConverter<T> : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                if (typeof(T) == typeof(int))
                {
                    return 0;
                }
                else if (typeof(T) == typeof(float))
                {
                    return 0.0f;
                }
                else if (typeof(T) == typeof(decimal))
                {
                    return 0.0m;
                }
                else if (typeof(T) == typeof(DateTime))
                {
                    return DateTime.MinValue;
                }
            }

            text = text.Trim();

            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(text, out int result))
                {
                    return result;
                }
            }
            else if (typeof(T) == typeof(string))
            {
                return text == "Y" ?"Yes":"No";
            }

            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
