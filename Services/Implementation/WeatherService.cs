using projetoGloboClima.Infrastructure.Interfaces;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace projetoGloboClima.Services.Implementation
{
    public class WeatherService
    {

        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        public async Task<List<CityResult>?> SearchCities(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return null;

            var cities = await _weatherRepository.SearchCities(cityName);

            if (cities == null || cities.Count == 0)
                return null;

            List<CityResult> listCities = new List<CityResult>();
            cities.RemoveAll(x => x.Name != cityName);

            return cities;
        }


        public async Task<WeatherResponse?> GetWeather(double lat, double lon)
        {

            WeatherResponse? weatherResponse = await _weatherRepository.GetWeather(lat, lon);

            if (weatherResponse != null)
                return weatherResponse;

            throw new KeyNotFoundException("Nenhuma cidade encontrada."); ;
        }

    }
}


