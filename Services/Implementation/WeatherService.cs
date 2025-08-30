using projetoGloboClima.Infrastructure.Interfaces;
using projetoGloboClima.Models.Entities;
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

        //public async Task<string> GetWeatherByCity(string city)
        //{

        //    var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
        //    var response = await _httpClient.GetAsync(url);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        WeatherResponse data = JsonSerializer.Deserialize<WeatherResponse>(json);

        //    }


        //    return await response.Content.ReadAsStringAsync();
        //}

        public async Task<List<CityResult>> SearchCitiesAsync(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                throw new ArgumentException("O nome da cidade não pode ser vazio.");

            var cities = await _weatherRepository.SearchCitiesAsync(cityName);

            if (cities == null || cities.Count == 0)
                throw new KeyNotFoundException("Nenhuma cidade encontrada.");

            List<CityResult> listCities = new List<CityResult>();
            cities.RemoveAll(x => x.Name != cityName);

            return cities;
        }


        public async Task<WeatherResponse?> GetWeatherAsync(double lat, double lon)
        {

            WeatherResponse? weatherResponse = await _weatherRepository.GetWeatherAsync(lat, lon);

            if (weatherResponse != null)
                return weatherResponse;

            throw new KeyNotFoundException("Nenhuma cidade encontrada."); ;
        }
    }
}


