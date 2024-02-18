using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Endpoints;

public static class ApiEndpoints
{
    public static void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/foodtrucks");

        group.MapGet("/", (IDataService ds, [FromQuery] int page = 0, [FromQuery] int pageSize = 10) =>
        {
            return ds.GetPage(page, pageSize);
        });

        group.MapGet("/all", (IDataService ds) =>
        {
            return ds.Records;
        });

        group.MapPost("/search", (
            IDataService ds,
            [FromBody] FoodTruckSearchPayload payload,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10) =>
        {
            return ds.Search(payload, page, pageSize);
        });
    }
}
