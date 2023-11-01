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
        var data = _service.ParseCsvData();
        var processed = _service.ProcesData(data);
        _service.DataStorage(processed);


    }
}