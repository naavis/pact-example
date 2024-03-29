﻿using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        [HttpGet("{location}")]
        public IActionResult GetWeather(string location)
        {
            if (location == "Helsinki")
            {
                return Ok(new Weather
                {
                    Temperature = -15.0f,
                    Humidity = 80.0f,
                    WindSpeed = 5.0f
                });
            }
            else
            {
                return NotFound(new { error = $"{location} is not a city, dummy!" });
            }
        }
    }
}
