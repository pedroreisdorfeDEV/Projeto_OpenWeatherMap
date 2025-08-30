using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Services.Interfaces;
using projetoGloboClima.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Shared.OutPut;
using projetoGloboClima.Shared.Utils;
using Amazon.DynamoDBv2.DocumentModel;
using System.Net.Http;
using System.Text.Json;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

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
            UserEntity? user = await _userRepository.GetLogin(email, password);

            if (user != null)
            {
                var token = _tokenGenerator.GenerateToken(user.Email, user.Name, user.UserId);

                return new AuthResult
                {
                    User = user,
                    Token = token
                };
            }

            return null; 
        }



    }
}
