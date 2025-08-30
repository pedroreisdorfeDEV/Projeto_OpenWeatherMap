using projetoGloboClima.Infrastructure.Interfaces;
using projetoGloboClima.Models.Entities;
using System.Net.Http;
using System.Text.Json;

namespace projetoGloboClima.Infrastructure.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherRepository(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenWeatherMap:ApiKey"];
        }

        public async Task<WeatherResponse?> GetWeatherAsync(double lat, double lon)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_apiKey}&units=metric&lang=pt_br";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var retorno =  JsonSerializer.Deserialize<WeatherResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return retorno;
        }

        public async Task<List<CityResult>?> SearchCitiesAsync(string cityName)
        {
            try
            {
                var url = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=5&appid={_apiKey}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                //response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                //List<CityResult> listCityResult = JsonSerializer.Deserialize<List<CityResult>>(json);

                var retorno = JsonSerializer.Deserialize<List<CityResult>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return retorno;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
