namespace Models;

public class FoodTruckSearchPayload
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string PreferredFood { get; set; } = null!;
}
