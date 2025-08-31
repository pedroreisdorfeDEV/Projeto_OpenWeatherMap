using Amazon.DynamoDBv2.DataModel;

namespace projetoGloboClima.Models.Entities
{
    public class FavoriteCity
    {
        [DynamoDBHashKey] // Partition Key
        public string UserId { get; set; } = string.Empty;

        [DynamoDBRangeKey] // Sort Key
        public string Cidade { get; set; } = string.Empty;
        [DynamoDBProperty]
        public string Pais { get; set; } = string.Empty;
        [DynamoDBProperty]
        public double Temperatura { get; set; }
        [DynamoDBProperty]
        public double SensacaoTermica { get; set; }
        [DynamoDBProperty]
        public double TempMin { get; set; }
        [DynamoDBProperty]
        public double TempMax { get; set; }
        [DynamoDBProperty]
        public int Umidade { get; set; }
        [DynamoDBProperty]
        public string Condicao { get; set; } = string.Empty;
        [DynamoDBProperty]
        public double VelocidadeVento { get; set; }
        [DynamoDBProperty]
        public int Nebulosidade { get; set; }
        [DynamoDBProperty]
        public DateTime DataFavorito { get; set; } = DateTime.UtcNow;
    }
}
