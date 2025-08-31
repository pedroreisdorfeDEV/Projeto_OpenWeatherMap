using projetoGloboClima.Infrastructure.Interfaces;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Services.Interfaces;

namespace projetoGloboClima.Services.Implementation
{
    public class FavoriteCityService : IFavoriteCityService
    {
        private readonly IFavoriteCityRepository _favoriteCityRepository;

        public FavoriteCityService(IFavoriteCityRepository favoriteCityRepository)
        {
            _favoriteCityRepository = favoriteCityRepository;
        }

        public async Task<bool> DeleteFavoriteCity(string userId, string city)
        {
            bool retorno = await _favoriteCityRepository.DeleteFavoriteCity(userId, city);
            return retorno;
        }

        public async Task<List<FavoriteCity>> GetFavoritesByUserId(string userId)
        {
            List<FavoriteCity> listFavoritesCities = await _favoriteCityRepository.GetFavoritesByUserId(userId);
            return listFavoritesCities;
        }
    }
}
