using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reec.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Reec.Inspection.ReecEnums;

namespace Reec.Api.Test.Publish.Controllers
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
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {


            try
            {
                var numerador = 1;
                var denominador = 0;
                var resultado = numerador / denominador;
            }
            catch (Exception ex)
            {
                throw new ReecException(Category.BusinessLogicLegacy, $"El sistema de EMOA produjo un error no controlado",  "Sistema EMOA", ex);
            }

          

            

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
