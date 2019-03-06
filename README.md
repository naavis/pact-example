Contract testing with Pact - Example code
=========================================
The example consists of an API endpoint and an API client, located in
_weather-api_ and _weather-client_ projects respectively.
The API serves weather information for Helsinki at `http://localhost:5000/weather/Helsinki`

Requests for any other location should fail.

You can start the API server by running `dotnet run -p weather-api`

The client that connects to the API can be executed by running `dotnet run -p weather-client`

Testing and Pact
----------------

Requires .NET Core 2.2. To compile, just run `dotnet build`

To run client tests and generate the Pact file, run the following:
```
cd weather-client-test
dotnet test
```

To then run API tests based on the Pact file, run the following:
```
cd weather-api-test
dotnet test
```
