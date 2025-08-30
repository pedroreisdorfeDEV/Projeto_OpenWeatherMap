using projetoGloboClima.Models.Entities;

namespace projetoGloboClima.Infrastructure.Interfaces
{
    public interface IWeatherRepository
    {
        public Task<List<CityResult>?> SearchCitiesAsync(string cityName);
        public Task<WeatherResponse?> GetWeatherAsync(double lat, double lon);
    }
}
