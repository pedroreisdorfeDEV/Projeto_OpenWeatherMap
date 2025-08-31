using projetoGloboClima.Models.Entities;

namespace projetoGloboClima.Services.Interfaces
{
    public interface IFavoriteCityService
    {
        public Task<List<FavoriteCity>> GetFavoritesByUserId(string userId);
        public Task<bool> DeleteFavoriteCity(string userId, string cityId);
    }
}
