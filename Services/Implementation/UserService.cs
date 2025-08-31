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
using projetoGloboClima.Models.ViewModels;

namespace projetoGloboClima.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _tokenGenerator;

        public UserService(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> CreateUser(UserEntity user)
        {
            // gera hash da senha antes de salvar
            user.Password = Shared.Utils.Hash.GerarHashSHA512(user.Password).ToUpper();

            bool retorno = await _userRepository.CreateUser(user);
            return retorno;
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

    
                AuthResult authResult = new AuthResult
                {
                    User = user,
                    Token = token
                };

                return authResult;

            }

            return null; 
        }

        public async Task<bool> AddFavoriteCity(string usuarioId, WeatherViewModel model)
        {
            if (string.IsNullOrEmpty(usuarioId) || model == null)
                return false;

            bool retorno = await _userRepository.AddFavoriteCity(usuarioId, model);
            return retorno;
        }

    }
}
