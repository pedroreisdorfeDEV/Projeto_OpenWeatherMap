using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Infrastructure.Interfaces;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Services.Implementation;
using projetoGloboClima.Services.Interfaces;
using projetoGloboClima.Shared.OutPut;
using projetoGloboClima.Shared.Utils;

namespace projetoGloboClima.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _context;

        public UserRepository(IDynamoDBContext context)
        {
            _context = context;
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

                return results.Count > 0 ? results[0] : null;
            }
            catch (Exception e)
            {

                throw;
            }

            //string senhaSHA512 = Shared.Utils.Hash.GerarHashSHA512(senha).ToUpper();

        }
    }
}
