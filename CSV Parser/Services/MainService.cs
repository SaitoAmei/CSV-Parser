using AutoMapper;
using CSV_Parser.Data.Maps;
using CSV_Parser.Data.Models;
using CsvHelper.Configuration;
using CsvHelper;
using EFCore.BulkExtensions;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace CSV_Parser.Services
{
    public class MainService : IMainService
    {
        readonly CsvParserDataContext _context;
        readonly MapperConfiguration _configMap;
        readonly IConfiguration _configDomain;
        readonly string _domain;


        public MainService()
        {
            _configMap = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EqualMapperProfile>();
            });
            _domain = AppContext.BaseDirectory.Split("\\bin").First();


            _configDomain = new ConfigurationBuilder()
                    .SetBasePath(this._domain)
                    .AddJsonFile("appsettings.json")
                    .Build();
            var connectionString = _configDomain.GetConnectionString("MyConnectionString");
            _context = new CsvParserDataContext(connectionString); ;

        }


        public void DataStorage(List<TaxiHistoryModel> data)
        {

            IMapper mapper = _configMap.CreateMapper();
            var tableModel = mapper.Map<List<TaxiHistoryModel>, List<OrdersHistory>>(data);
            _context.BulkInsert(tableModel.Take(1));
            _context.SaveChanges();
            Console.WriteLine($"Data inserted successfully - {tableModel.Count} rows");


        }

        public List<TaxiHistoryModel> ParseCsvData()
        {
            Console.WriteLine($"Starting extracting data...");

            var records = new List<TaxiHistoryModel>();
            var csvFilePath = Path.Combine(_domain, @"Data\Source\sample-cab-data.csv");

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))))
            {

                csv.Context.RegisterClassMap<TaxiHistoryMapper>();
                records = csv.GetRecords<TaxiHistoryModel>().ToList();

            }
            Console.WriteLine($"Data extracted successfully! - {records.Count} rows");

            return records;
        }

        public List<TaxiHistoryModel> ProcesData(List<TaxiHistoryModel> data)
        {
            Console.WriteLine("Starting filtering data...");

            var filePath = Path.Combine(_domain, @"Data\Output\output.csv");
            var duplicatedRecords = data.GroupBy(r => new
            {
                r.TpepPickupDatetime,
                r.TpepDropoffDatetime,
                r.PassengerCount
            }).Where(group => group.Count() > 1).SelectMany(group => group).ToList();

            if (duplicatedRecords.Any())
            {
                Console.WriteLine($"Found duplictated  data! - {duplicatedRecords.Count} records\nSaving duplicates into output.csv...");
            }
            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.GetCultureInfo("en-US"))))
            {
                csv.WriteRecords(duplicatedRecords);
                writer.Flush();

                File.WriteAllBytes(filePath, memoryStream.ToArray());
                Console.WriteLine("File saved successfully.");

            }
            return data.Except(duplicatedRecords).ToList();
        }
    }
}
