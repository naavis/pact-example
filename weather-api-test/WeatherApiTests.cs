using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System.Net.Http;
using WeatherApi;
using System.Net;
using PactNet;
using System.Collections.Generic;

namespace WeatherApi.Test
{
    [TestClass]
    public class WeatherApiTests
    {
        private static IWebHost _webHost;

        [ClassInitialize]
        public static void Setup(TestContext _)
        {
            _webHost = WebHost.CreateDefaultBuilder()
                    .UseStartup<Startup>()
                    .Build();

            _webHost.Start();
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            _webHost.StopAsync().GetAwaiter().GetResult();
            _webHost.Dispose();
        }

        [TestMethod]
        public void CallApi()
        {
            var pactVerifierConfig = new PactVerifierConfig();
            var pactVerifier = new PactVerifier(pactVerifierConfig);
            pactVerifier
                .ServiceProvider("Weather API", "http://localhost:5000")
                .HonoursPactWith("Weather API Client")
                .PactUri("../../../../pacts/weather_api_client-weather_api.json")
                .Verify();
        }
    }
}
