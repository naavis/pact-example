
using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace WeatherClient.Test
{
    public class WeatherApiPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort { get { return 9222; } }
        public string MockProviderServiceBaseUri { get { return String.Format("http://localhost:{0}", MockServerPort); } }

        public WeatherApiPact()
        {
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir = @"..\..\..\..\pacts",
                LogDir = @"..\..\..\..\logs"
            };
            PactBuilder = new PactBuilder(pactConfig);

            PactBuilder
                .ServiceConsumer("Weather API Client")
                .HasPactWith("Weather API");
            
            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}