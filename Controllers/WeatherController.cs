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

        [HttpGet]
        public async Task<IActionResult> SearchCities(CityRequestViewModel cityView)
        {
            var cities = await _weatherService.SearchCitiesAsync(cityView.City);
            var citySearchViewModel = new CitySearchViewModel
            {
                City = cityView.City,
                Cities = cities
            };

            return View("IndexWeather", citySearchViewModel);
        } 

        [HttpGet]
        public async Task<IActionResult> GetWeather(double lat, double lon)
        {
            var weather = await _weatherService.GetWeatherAsync(lat, lon);

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

        // GET: WeatherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WeatherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeatherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WeatherController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WeatherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WeatherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WeatherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
