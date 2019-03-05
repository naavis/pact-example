using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherClient
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient(string apiUrl)
        {
            _client = new HttpClient { BaseAddress = new Uri(apiUrl) };
        }

        public async Task<Weather> GetWeather(string location)
        {
            string reasonPhrase;

            var request = new HttpRequestMessage(HttpMethod.Get, "/weather/" + location);
            request.Headers.Add("Accept", "application/json");

            var response = await _client.SendAsync(request);
            var status = response.StatusCode;

            reasonPhrase = response.ReasonPhrase; //NOTE: any Pact mock provider errors will be returned here and in the response body

            if (status == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return !String.IsNullOrEmpty(content) ? JsonConvert.DeserializeObject<Weather>(content) : null;
            }

            throw new Exception(reasonPhrase);

        }
    }
}