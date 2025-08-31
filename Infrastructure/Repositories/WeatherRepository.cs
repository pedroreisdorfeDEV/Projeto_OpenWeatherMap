using Humanizer;
using projetoGloboClima.Infrastructure.Interfaces;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;
using System.Net.Http;
using System.Text.Json;

namespace projetoGloboClima.Infrastructure.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ILogger<WeatherRepository> _logger;

        public WeatherRepository(IConfiguration configuration, ILogger<WeatherRepository> logger)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenWeatherMap:ApiKey"];
            _logger = logger;
        }


        public async Task<WeatherResponse?> GetWeatherAsync(double lat, double lon)
        {

            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_apiKey}&units=metric&lang=pt_br";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Falha ao buscar clima", response.StatusCode);

                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();

                var retorno = JsonSerializer.Deserialize<WeatherResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _logger.LogInformation($"Clima obtido com sucesso para latitude {lat} e longitude {lon}", lat, lon);

                return retorno;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro ao buscar clima para latitude {lat} e longitude {lon}", e);

                throw;
            }
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

                var json = await response.Content.ReadAsStringAsync();


                var retorno = JsonSerializer.Deserialize<List<CityResult>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return retorno;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro ao buscar clima para cidades {cityName}", e);

                throw;
            }
        }


    }
}
