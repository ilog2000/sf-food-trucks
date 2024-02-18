using CsvHelper.Configuration.Attributes;
using Globals;

namespace Models;

public class FoodTruckRecord
{
    [Name(Constants.LocationId)]
    public string? LocationId { get; set; }
    [Name(Constants.Applicant)]
    public string? Name { get; set; }
    public string? FacilityType { get; set; }
    public string? LocationDescription { get; set; }
    public string? Address { get; set; }
    public string Status { get; set; } = "UNKNOWN";
    public string FoodItems { get; set; } = string.Empty;
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string? Location { get; set; }
}
