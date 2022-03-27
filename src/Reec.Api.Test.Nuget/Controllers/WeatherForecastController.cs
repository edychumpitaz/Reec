using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reec.Api.Test.Nuget.Controllers
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


            //List<string> mensajes = new List<string> { "Regla de negocio", "valicación de campos", "desde nuget local"};
            //throw new Reec.Inspection.ReecException(Inspection.ReecEnums.Category.Warning, mensajes);


            //throw new Reec.Inspection.ReecException(Inspection.ReecEnums.Category.BusinessLogic, "Regla de negocio");

            var numerador = 1;
            var denominador = 0;
            var resultado = numerador / denominador;

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
