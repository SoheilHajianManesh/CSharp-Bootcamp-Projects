using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Task2.Service;

public class CsvReaderService
{
    public List<T> ReadFromCsv<T>(string filePath)
    {
        var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
        };
        Console.WriteLine(filePath);
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, configuration);
        var records = csv.GetRecords<T>();
        return records.ToList();
    }
}