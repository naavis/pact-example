using System;

namespace weather_client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ApiClient("http://localhost:5000/");
            var weather = client.GetWeather("Helsinki");
            Console.WriteLine(weather);
        }
    }
}
