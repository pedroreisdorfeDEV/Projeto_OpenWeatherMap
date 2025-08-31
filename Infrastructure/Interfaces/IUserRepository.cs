using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;
using projetoGloboClima.Shared.Utils;

namespace projetoGloboClima.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity?> GetLogin(string login, string senha);
        
        public Task<bool> AddFavoriteCity(string usuarioId, WeatherViewModel model);

        public Task<bool> CreateUser(UserEntity user);
    }
}
