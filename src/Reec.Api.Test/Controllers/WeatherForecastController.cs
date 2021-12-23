using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reec.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reec.Api.Test.Controllers
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
            var rng = new Random();


            //if (true)
            //{
            //    throw new ReecException(Inspection.ReecEnums.Category.BusinessLogic, "regla de negocio 1.");

            //}


            //List<string> mensajes = new List<string> { "Regla de negocio", "valicación de campos" };
            //throw new ReecException(Inspection.ReecEnums.Category.Warning, mensajes);



            var numerador = 1;
            var denominador = 0;
            var division = numerador / denominador;


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
