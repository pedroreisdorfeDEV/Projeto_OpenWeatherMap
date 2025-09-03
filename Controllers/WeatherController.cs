using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;
using projetoGloboClima.Services.Implementation;

namespace projetoGloboClima.Controllers
{

    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        // GET: WeatherController
        public async Task<IActionResult> IndexWeather()
        {
            return View();
        }

        /// <summary>
        /// Faz busca por cidade na api openweathermap
        /// </summary>
        [HttpGet("api/SearchCities")]
        [ProducesResponseType(typeof(CitySearchViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchCities([FromQuery] string city)
        {
            var cities = await _weatherService.SearchCities(city);

            if (cities == null || cities.Count == 0)
                return NotFound(new { message = "Cidade sem informações climáticas. Refaça sua busca!" });

            var result = new CitySearchViewModel
            {
                City = city,
                Cities = cities
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchCities(CityRequestViewModel cityView)
        {
            var cities = await _weatherService.SearchCities(cityView.City);

            if (cities == null || cities.Count == 0)
            {
                ViewBag.PopupMensagem = "Cidade sem informações climáticas. Refaça sua busca!";
                return View("IndexWeather");
            }



                var citySearchViewModel = new CitySearchViewModel
            {
                City = cityView.City,
                Cities = cities
            };

            return View("IndexWeather", citySearchViewModel);
        }


        /// <summary>
        /// Obtém informações climáticas de uma cidade a partir da latitude e longitude.
        /// </summary>
        [HttpGet("api/GetWeather")]
        [ProducesResponseType(typeof(WeatherViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWeatherApi([FromQuery] double lat, [FromQuery] double lon)
        {
            var weather = await _weatherService.GetWeather(lat, lon);

            if (weather == null)
                return NotFound(new { message = "Não foi possível obter os dados de clima." });

            var viewModel = new WeatherViewModel
            {
                Cidade = weather.Name,
                Pais = weather.Sys.Country,
                Temperatura = weather.Main.Temp,
                SensacaoTermica = weather.Main.FeelsLike,
                TempMin = weather.Main.TempMin,
                TempMax = weather.Main.TempMax,
                Umidade = weather.Main.Humidity,
                Condicao = weather.Weather.FirstOrDefault()?.Description,
                VelocidadeVento = weather.Wind.Speed,
                Nebulosidade = weather.Clouds.All
            };

            return Ok(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeather(double lat, double lon)
        {
            var weather = await _weatherService.GetWeather(lat, lon);

            var citySearchViewModel = new CitySearchViewModel
            {
                Weather = weather
            };

            var viewModel = new WeatherViewModel
            {
                Cidade = weather.Name,
                Pais = weather.Sys.Country,
                Temperatura = weather.Main.Temp,
                SensacaoTermica = weather.Main.FeelsLike,
                TempMin = weather.Main.TempMin,
                TempMax = weather.Main.TempMax,
                Umidade = weather.Main.Humidity,
                Condicao = weather.Weather.FirstOrDefault()?.Description,
                VelocidadeVento = weather.Wind.Speed,
                Nebulosidade = weather.Clouds.All
            };

            return View("WeatherDetails", viewModel);
        }


    }
}
