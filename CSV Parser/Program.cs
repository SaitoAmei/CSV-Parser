using CSV_Parser.Data.Maps;
using CSV_Parser.Data.Models;
using CSV_Parser.Services;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;


internal class Program
{
    private static void Main(string[] args)
    {

        var _service = new MainService();
        var extractedData = _service.ParseCsvData();
        var filteredData = _service.ProcesData(extractedData);
        _service.DataStorage(filteredData);


    }
}