namespace projetoGloboClima.Models.ViewModels
{
    public class WeatherViewModel
    {
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public double Temperatura { get; set; }
        public double SensacaoTermica { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Umidade { get; set; }
        public string Condicao { get; set; } 
        public double VelocidadeVento { get; set; }
        public int Nebulosidade { get; set; }
    }
}
