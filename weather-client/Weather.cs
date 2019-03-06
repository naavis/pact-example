public class Weather
{
    public float Temperature { get; set; }
    public float Humidity { get; set; }
    public float WindSpeed { get; set; }

    public override string ToString()
    {
        return $"Temperature: {Temperature}°C, Humidity: {Humidity}%, Wind speed: {WindSpeed} m/s";
    }
}
