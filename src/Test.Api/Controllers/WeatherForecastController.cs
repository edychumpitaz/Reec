using Microsoft.AspNetCore.Mvc;
using Reec.Inspection;
using static Reec.Inspection.ReecEnums;

namespace Test.Api.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(nameof(TestWarning))]
        public IActionResult TestWarning(string parameter)
        {
            // Error controlado de validación de datos
            if (!string.IsNullOrWhiteSpace(parameter))
                throw new ReecException(Category.Warning, "Campo 'parameter' de prueba.");

            return Ok(parameter);
        }

        [HttpPost(nameof(TestBusinessLogic))]
        public IActionResult TestBusinessLogic(TestBusinessLogicRequest request)
        {

            throw new ReecException(Category.BusinessLogic, "Prueba de validacón de lógica de negocio.");

        }

    }

    public class TestBusinessLogicRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
