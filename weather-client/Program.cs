using System;

namespace WeatherClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ApiClient("http://localhost:5000/");
            var weather = client.GetWeather("Helsinki").Result;
            Console.WriteLine(weather);
        }
    }
}
