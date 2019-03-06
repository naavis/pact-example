using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace WeatherClient.Test
{
    [TestClass]
    public class ApiClientTests
    {
        #region Setup & Private

        private static WeatherApiPact _pact;
        private static IMockProviderService _mockService;
        private static string _mockServiceUri;

        [ClassInitialize]
        public static void SetupPact(TestContext _)
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
                        temperature = -15.0,
                        humidity = 80.0,
                        windSpeed = 5.0
                    }
                });

            var api = new ApiClient(_mockServiceUri);

            var result = api.GetWeather("Helsinki").Result;

            Assert.AreEqual(-15.0, result.Temperature);
            Assert.AreEqual(80.0, result.Humidity);
            Assert.AreEqual(5.0, result.WindSpeed);

            _mockService.VerifyInteractions();
        }

        [TestMethod]
        public void GetWeather_InvalidCity_ReturnsError()
        {
            _mockService
                .Given("Holsinki is not a valid city")
                .UponReceiving("A GET request to retrieve weather for Holsinki")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/weather/Holsinki",
                    Headers = new Dictionary<string, object>
                    {
                        { "Accept", "application/json"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 404,
                    Headers = new Dictionary<string, object>
                    {
                        {"Content-Type", "application/json; charset=utf-8"}
                    },
                    Body = new
                    {
                        error = "Holsinki is not a city, dummy!"
                    }
                });

            var api = new ApiClient(_mockServiceUri);

            Assert.ThrowsExceptionAsync<AggregateException>(async () => await api.GetWeather("Holsinki"));

            _mockService.VerifyInteractions();
        }

        [ClassCleanup]
        public static void CleanupPact()
        {
            _pact.Dispose();
        }
    }
}
