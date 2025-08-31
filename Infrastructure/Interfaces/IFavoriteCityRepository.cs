using projetoGloboClima.Models.Entities;

namespace projetoGloboClima.Infrastructure.Interfaces
{
    public interface IFavoriteCityRepository
    {
        public Task<List<FavoriteCity>> GetFavoritesByUserId(string userId);
        public Task<bool> DeleteFavoriteCity(string userId, string city);
    }
}
