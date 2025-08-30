using Amazon.DynamoDBv2.DataModel;

namespace projetoGloboClima.Models.Entities
{
    public class FavoriteCity
    {
        [DynamoDBHashKey] // Partition Key
        public string UserId { get; set; } = string.Empty;

        [DynamoDBRangeKey] // Sort Key
        public string Cidade { get; set; } = string.Empty;

        public string Pais { get; set; } = string.Empty;
        public double Temperatura { get; set; }
        public double SensacaoTermica { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Umidade { get; set; }
        public string Condicao { get; set; } = string.Empty;
        public double VelocidadeVento { get; set; }
        public int Nebulosidade { get; set; }
        public DateTime DataFavorito { get; set; } = DateTime.UtcNow;
    }
}
