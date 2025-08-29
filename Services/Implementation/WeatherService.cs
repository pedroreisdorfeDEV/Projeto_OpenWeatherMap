using projetoGloboClima.Models.Entities;
using System.Net.Http;
using System.Text.Json;

namespace projetoGloboClima.Services.Implementation
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenWeatherMap:ApiKey"];
        }

        public async Task<string> GetWeatherByCity(string city)
        {
            city = "London";
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);



            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                WeatherResponse data = JsonSerializer.Deserialize<WeatherResponse>(json);

            }
               

            return await response.Content.ReadAsStringAsync();
        }
    }

}
