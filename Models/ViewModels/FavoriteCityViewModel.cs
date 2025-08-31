namespace projetoGloboClima.Models.ViewModels
{
    public class FavoriteCityViewModel
    {
        public string CityId { get; set; }   // ou int, dependendo do DynamoDB
        public string CityName { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        // informações do clima
        public double Temperatura { get; set; }
        public double SensacaoTermica { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Umidade { get; set; }
        public string Condicao { get; set; }
        public double VelocidadeVento { get; set; }
        public int Nebulosidade { get; set; }
        public DateTime DataFavorito { get; set; }
    }
}
