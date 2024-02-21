using Models;
using Services;

namespace WebAPITests;

public class DataServiceTests
{
    [Fact]
    public void SearchTest()
    {
        // Arrange
        IDataService dataService = new DataService();
        dataService.Load("dataset.csv");
        var payload = new FoodTruckSearchPayload
        {
            Latitude = 37.7749f,
            Longitude = -122.4194f,
            PreferredFood = "sandwiches"
        };

        // Act
        var result = dataService.Search(payload, 0, 10).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.True(result.Count == 5);
        Assert.True(result.All(r => r.Status == "APPROVED"));
        Assert.Equal(2, result.Count(r => r.FoodItems.Contains("sandwiches", StringComparison.OrdinalIgnoreCase)));
        Assert.Contains("sandwiches", result[0].FoodItems, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("sandwiches", result[1].FoodItems, StringComparison.OrdinalIgnoreCase);
    }
}
