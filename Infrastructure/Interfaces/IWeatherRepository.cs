using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;

namespace projetoGloboClima.Infrastructure.Interfaces
{
    public interface IWeatherRepository
    {
        public Task<List<CityResult>?> SearchCities(string cityName);
        public Task<WeatherResponse?> GetWeather(double lat, double lon);

    }
}
