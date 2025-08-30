using projetoGloboClima.Models.Entities;

namespace projetoGloboClima.Models.ViewModels
{
    public class CitySearchViewModel
    {
        public string? City { get; set; }
        public List<CityResult> Cities { get; set; }
        public WeatherResponse Weather { get; set; }
    }
}
