
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using CsvHelper;
using System.ComponentModel.DataAnnotations;

namespace CSV_Parser.Data.Models
{
    public class TaxiHistoryModel
    {
        public DateTime TpepPickupDatetime { get; set; }
        public DateTime TpepDropoffDatetime { get; set; }
        public int PassengerCount { get; set; }
        public float TripDistance { get; set; }
        public string? StoreAndFwdFlag { get; set; }
        public int PULocationID { get; set; }
        public int DOLocationID { get; set; }
        public decimal FareAmount { get; set; }
        public decimal TipAmount { get; set; }
    }
}
