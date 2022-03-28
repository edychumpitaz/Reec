using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reec.Inspection;
using System;
using System.Collections.Generic;
using System.Linq;
using static Reec.Inspection.ReecEnums;

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

        [HttpGet("Get")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();


            //if (true)
            //{
            //    throw new ReecException(Inspection.ReecEnums.Category.BusinessLogic, "regla de negocio 1.");

            //}

            throw new ReecException(Inspection.ReecEnums.Category.PartialContent, new List<string> { "La consulta no contiene registros." });

            //List<string> mensajes = new List<string> { "Regla de negocio", "valicación de campos" };
            //throw new ReecException(Inspection.ReecEnums.Category.Warning, mensajes);



            //var numerador = 1;
            //var denominador = 0;
            //var division = numerador / denominador;


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

        }


        [HttpGet("TestWarning")]
        public IActionResult TestWarning(string parameter)
        {
            // Error controlado de validación de datos
            if (string.IsNullOrWhiteSpace(parameter))
                throw new ReecException(Category.Warning, $"Campo '{nameof(parameter)}' obligatorio.");

            return Ok(parameter);
        }

        [HttpGet("TestBusinessLogic")]
        public IActionResult TestBusinessLogic(string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                throw new ReecException(Category.BusinessLogic, "No cumple con la regla de negocio.");
            return Ok(parameter);
        }

        [HttpGet("TestBusinessLogicLegacy")]
        public IActionResult TestBusinessLogicLegacy(string parameter)
        {
            try
            {
                var numerador = 1;
                var denominador = 0;
                var division = numerador / denominador;
                return Ok(parameter);
            }
            catch (Exception ex)
            {
                throw new ReecException(Category.BusinessLogicLegacy, "Error no controlado del sistema legacy 'app1'.", ex.Message);
            }
        }


        [HttpGet("TestInternalServerError/{parameter}")]
        public IActionResult TestInternalServerError(string parameter)
        {
            var numerador = 1;
            var denominador = 0;
            var division = numerador / denominador;
            return Ok(parameter);
        }




    }
}
