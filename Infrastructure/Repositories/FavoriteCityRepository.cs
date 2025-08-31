using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using projetoGloboClima.Infrastructure.Interfaces;
using projetoGloboClima.Models.Entities;
using System.Collections.Generic;

namespace projetoGloboClima.Infrastructure.Repositories
{
    public class FavoriteCityRepository : IFavoriteCityRepository
    {
        private readonly IDynamoDBContext _dynamoDb;
        private readonly ILogger<FavoriteCityRepository> _logger;

        public FavoriteCityRepository(IDynamoDBContext dynamoDb, ILogger<FavoriteCityRepository> logger)
        {
            _dynamoDb = dynamoDb;
            _logger = logger;
        }

        public async Task<List<FavoriteCity>> GetFavoritesByUserId(string userId)
        {
            try
            {
                var conditions = new List<ScanCondition>
                {
                    new ScanCondition("UserId", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, userId)
                };

                List<FavoriteCity> listFavoritesCities = await _dynamoDb.ScanAsync<FavoriteCity>(conditions).GetRemainingAsync();
                if(listFavoritesCities.Count > 0)
                {
                    _logger.LogInformation($"Encontradas cidades favoritas para o usuário {userId}", listFavoritesCities.Count, userId);
                }


                return listFavoritesCities;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro ao buscar cidades favoritas do usuário {userId}", userId);
                throw;
            }
        }

        public async Task<bool> DeleteFavoriteCity(string userId, string city)
        {
            try
            {
                await _dynamoDb.DeleteAsync<FavoriteCity>(userId, city);
                _logger.LogInformation($"Exclusão da cidade {city} feita com sucesso", userId);
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Erro ao tentar deletar cidade {city} do usuário {userId}", e);
                return false;
            }
        }
    }
}
