using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace WeatherClient.Test
{
    [TestClass]
    public class ApiClientTests : IDisposable
    {
        #region Constructor & Privates
        private WeatherApiPact _pact;
        private IMockProviderService _mockService;
        private string _mockServiceUri;

        public ApiClientTests()
        {
            _pact = new WeatherApiPact();
            _mockService = _pact.MockProviderService;
            _mockService.ClearInteractions();
            _mockServiceUri = _pact.MockProviderServiceBaseUri;
        }
        #endregion

        [TestMethod]
        public void GetWeather_CityExists_ReturnsWeather()
        {
            _mockService
                .Given("Helsinki is a valid city")
                .UponReceiving("A GET request to retrieve weather for Helsinki")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/weather/Helsinki",
                    Headers = new Dictionary<string, object>
                    {
                        {"Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new
                    {
                        Temperature = "-15.0",
                        Humidity = "80.0",
                        WindSpeed = "5.0"
                    }
                });
            
            var apiClient = new ApiClient(_mockServiceUri);

            var result = apiClient.GetWeather("Helsinki").Result;

            Assert.AreEqual(-15.0, result.Temperature);
            Assert.AreEqual(80.0, result.Humidity);
            Assert.AreEqual(5.0, result.WindSpeed);

            _mockService.VerifyInteractions();
        }

        public void Dispose()
        {
            _pact.Dispose();
        }
    }
}
