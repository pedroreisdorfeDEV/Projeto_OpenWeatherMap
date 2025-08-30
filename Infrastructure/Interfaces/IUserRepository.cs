using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;
using projetoGloboClima.Shared.Utils;

namespace projetoGloboClima.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserEntity?> GetLogin(string login, string senha);

        Task<bool> AddFavoriteCity(string usuarioId, WeatherViewModel model);
    }
}
