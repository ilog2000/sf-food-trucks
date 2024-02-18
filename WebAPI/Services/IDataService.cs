using Models;

namespace Services;

public interface IDataService
{
    IEnumerable<FoodTruckRecord> Records{ get; }
    void Load();
    IEnumerable<FoodTruckRecord> GetPage(int page, int pageSize);
    IEnumerable<FoodTruckRecord> Search(FoodTruckSearchPayload payload, int page, int pageSize);
}
