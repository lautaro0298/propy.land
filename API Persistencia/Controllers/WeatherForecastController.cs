using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_Persistencia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            if (_logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            if (Summaries == null)
            {
                throw new InvalidOperationException("Summaries array is null.");
            }

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = GetRandomNumber(rng),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private int GetRandomNumber(Random rng)
        {
            int result = rng.Next(-20, 55);
            if (result < 0)
            {
                _logger.LogWarning("Random number is negative: {Number}", result);
                return 0;
            }
            return result;
        }
    }
}
