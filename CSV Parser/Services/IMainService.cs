
using CSV_Parser.Data.Models;

namespace CSV_Parser.Services
{
    public interface IMainService
    {
        void DataStorage(List<TaxiHistoryModel> data);
        List<TaxiHistoryModel> ProcesData(List<TaxiHistoryModel> data);
        List<TaxiHistoryModel> ParseCsvData();
    }
}
