using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;
using projetoGloboClima.Shared.Utils;

namespace projetoGloboClima.Services.Interfaces
{
    public interface IUserService
    {
        public Task CreateUser(UserEntity user);
        public Task<UserEntity?> GetLogin(string login, string senha);
        //public Task<AuthResult?> Authenticate(string email, string password);
        public Task<AuthResult?> LoginAndGenerateToken(string email, string password);
        public Task<bool> AddFavoriteCity(string usuarioId, WeatherViewModel model);
    }
}
