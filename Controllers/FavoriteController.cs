using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Infrastructure.Repositories;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Services.Interfaces;
using projetoGloboClima.Shared.OutPut;

namespace projetoGloboClima.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteCityService _favoriteCityService;
        private readonly IOutputPort _outPutPort;


        public FavoriteController(IFavoriteCityService favoriteCityService, IOutputPort outputPort)
        {
            _favoriteCityService = favoriteCityService;
            _outPutPort = outputPort;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirst("userId")?.Value;

            var favorites = await _favoriteCityService.GetFavoritesByUserId(userId);

            if (favorites == null || favorites.Count == 0)
            {
                ViewBag.Message = "Usuário não contém cidades favoritas.";
                return View(new List<FavoriteCity>());
            }


            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Cidade)
        {
            string userId = User.FindFirst("userId")?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(Cidade))
                return Unauthorized();

            bool retorno = await _favoriteCityService.DeleteFavoriteCity(userId, Cidade);
            if (retorno == false)
            {

                ViewBag.Message = "Não foi possível exlcuir cidade favorita!";
                RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
