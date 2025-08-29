namespace projetoGloboClima.Models.Entities
{
    public class WeatherResult
    {
        public string CityName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string WeatherDescription { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
    }
}
