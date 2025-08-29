using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Services.Interfaces;
using projetoGloboClima.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Shared.OutPut;
using projetoGloboClima.Shared.Utils;
using Amazon.DynamoDBv2.DocumentModel;

namespace projetoGloboClima.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly IDynamoDBContext _context;

        public UserService(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator, IDynamoDBContext context)
        {
            _userRepository = userRepository;
            _tokenGenerator = jwtTokenGenerator;
            _context = context;
        }

        public Task CreateUser(UserEntity user)
        {
            throw new NotImplementedException();
        }


        public async Task<UserEntity?> GetLogin(string login, string senha)
        {
            UserEntity? user = await _userRepository.GetLogin(login, senha);

            return user;
        }


        public async Task<AuthResult?> LoginAndGenerateToken(string email, string password)
        {
            string senhaHash = Hash.GerarHashSHA512(password).ToUpper();

            var conditions = new List<ScanCondition>
                {
                    new ScanCondition("Email", ScanOperator.Equal, email),
                    new ScanCondition("Password", ScanOperator.Equal, senhaHash)
                };

            var search = _context.ScanAsync<UserEntity>(conditions);
            var results = await search.GetNextSetAsync();

            if (results.Count > 0)
            {
                UserEntity user = results[0];

                // Gera token
                var token = _tokenGenerator.GenerateToken(user.Email, user.Name, user.UserId);

                return new AuthResult
                {
                    User = user,
                    Token = token
                };
            }

            return null; // usuário não encontrado
        }

    }
}
