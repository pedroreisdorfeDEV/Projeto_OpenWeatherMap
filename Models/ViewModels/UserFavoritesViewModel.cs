namespace projetoGloboClima.Models.ViewModels
{
    public class UserFavoritesViewModel
    {
        public string UserId { get; set; }
        public List<FavoriteCityViewModel> FavoriteCities { get; set; } = new();
    }
}
