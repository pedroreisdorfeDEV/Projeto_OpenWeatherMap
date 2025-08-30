using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;

namespace projetoGloboClima.Infrastructure.Interfaces
{
    public interface IWeatherRepository
    {
        public Task<List<CityResult>?> SearchCitiesAsync(string cityName);
        public Task<WeatherResponse?> GetWeatherAsync(double lat, double lon);

    }
}
