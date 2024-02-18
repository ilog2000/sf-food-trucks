using CsvHelper;
using Globals;
using Models;

namespace Services;

public class DataService : IDataService
{
    private Lazy<IEnumerable<FoodTruckRecord>> _lazyRecords = new Lazy<IEnumerable<FoodTruckRecord>>(() => LoadRecords());

    public IEnumerable<FoodTruckRecord> Records { get { return _lazyRecords.Value; } }

    private static IEnumerable<FoodTruckRecord> LoadRecords()
    {
        using var reader = new StreamReader(Constants.CsvFileName);
        using var csv = new CsvReader(reader, CsvReaderConfig.DefaultCsvConfig);
        return csv.GetRecords<FoodTruckRecord>().ToList();
    }

    public void Load()
    {
        _lazyRecords = new Lazy<IEnumerable<FoodTruckRecord>>(() => LoadRecords());
    }

    public IEnumerable<FoodTruckRecord> GetPage(int page, int pageSize)
    {
        // page is 0-based
        return Records.GetPage(page, pageSize);
    }

    public IEnumerable<FoodTruckRecord> Search(FoodTruckSearchPayload payload, int page, int pageSize)
    {
        return Records
            .Where(r => r.Status == "APPROVED")
            .OrderByDescending(r => Matches(r.FoodItems, payload.PreferredFood))
            .ThenBy(r => Distance(r.Latitude, r.Longitude, payload.Latitude, payload.Longitude))
            .GetPage(page, pageSize);
    }

    private static int Matches(string content, string search)
    {
        char[] separators = [',', '/', ' ', ';'];
        var keywords = search.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        if (keywords.Length == 0) return 0;
        return keywords.Count(k => content.Contains(k, StringComparison.OrdinalIgnoreCase));
    }

    private static float Distance(float lat1, float lon1, float lat2, float lon2)
    {
        // IMPORTANT !!!
        // We use a simple formula to calculate the distance between two points on the Earth's surface:
        // = sqrt((lat2-lat1)^2+(lon2-lon1)^2)*111.32 (where 111.32 is the number of kilometers in one degree).
        // More precise calculations can be done with the formula:
        // = acos(sin(lat1)*sin(lat2)+cos(lat1)*cos(lat2)*cos(lon2-lon1))*6371 (where 6371 is Earth radius in km),
        // but for our purposes, the simple formula is enough.
        return (float)Math.Sqrt(Math.Pow(lat2 - lat1, 2) + Math.Pow(lon2 - lon1, 2)) * 111.32f;
    }
}

public static class DataServiceExtensions
{
    public static IEnumerable<FoodTruckRecord> GetPage(this IEnumerable<FoodTruckRecord> records, int page, int pageSize)
    {
        return records.Skip(page * pageSize).Take(pageSize);
    }
}
