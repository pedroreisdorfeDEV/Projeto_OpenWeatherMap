using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Infrastructure.Interfaces;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;
using projetoGloboClima.Services.Implementation;
using projetoGloboClima.Services.Interfaces;
using projetoGloboClima.Shared.OutPut;
using projetoGloboClima.Shared.Utils;

namespace projetoGloboClima.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IDynamoDBContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;   
        }


        public async Task<UserEntity?> GetLogin(string email, string senha)
        {
            UserEntity? userEntity = null;
            string senhaSHA512 = Shared.Utils.Hash.GerarHashSHA512(senha).ToUpper();
        
            try
            {
                var conditions = new List<ScanCondition>
                {
                    new ScanCondition("Email", ScanOperator.Equal, email),
                    new ScanCondition("Password", ScanOperator.Equal, senhaSHA512)
                };

                var search = _context.ScanAsync<UserEntity>(conditions);
               
                var results = await search.GetNextSetAsync();

                if (results.Count > 0)
                {
                    _logger.LogInformation($"Login bem-sucedido para {email}", email);
                    return results[0];
                }

                _logger.LogWarning($"Falha no login: usuário ou senha incorretos para {email}", email);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao tentar login para {Email}", email);
                throw;
            }
        }

        public async Task<bool> AddFavoriteCity(string userId, WeatherViewModel model)
        {
            try
            {
                var favorito = new FavoriteCity
                {
                    UserId = userId,
                    Cidade = model.Cidade,
                    Pais = model.Pais,
                    Temperatura = model.Temperatura,
                    SensacaoTermica = model.SensacaoTermica,
                    TempMin = model.TempMin,
                    TempMax = model.TempMax,
                    Umidade = model.Umidade,
                    Condicao = model.Condicao,
                    VelocidadeVento = model.VelocidadeVento,
                    Nebulosidade = model.Nebulosidade,
                    DataFavorito = DateTime.UtcNow
                };

                await _context.SaveAsync(favorito);
                _logger.LogInformation($"Cidade favorita {model.Cidade} adicionada com sucesso para o usuário {userId}", model.Cidade, userId);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro ao adicionar cidade favorita {model.Cidade} para o usuário {userId}", model.Cidade, userId);

                return false;
            }
        }
    }
}
