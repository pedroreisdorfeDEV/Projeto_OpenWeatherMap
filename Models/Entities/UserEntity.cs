using Amazon.DynamoDBv2.DataModel;

namespace projetoGloboClima.Models.Entities
{
    [DynamoDBTable("Users")]
    public class UserEntity
    {
        [DynamoDBHashKey] // Chave primária
        public string UserId { get; set; } = default!;

        [DynamoDBProperty]
        public string User { get; set; } = default!;

        [DynamoDBProperty]
        public string Name { get; set; } = default!;

        [DynamoDBProperty]
        public string Email { get; set; } = default!;

        [DynamoDBProperty]
        public string Password { get; set; } = default!;

    }
}
