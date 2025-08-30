using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Infrastructure.Interfaces;
using projetoGloboClima.Models.Entities;
using projetoGloboClima.Models.ViewModels;
using projetoGloboClima.Services.Implementation;
using projetoGloboClima.Services.Interfaces;
using projetoGloboClima.Shared.OutPut;
using projetoGloboClima.Shared.Utils;

namespace projetoGloboClima.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOutputPort _outPutPort;
        private readonly WeatherService _weatherService;

        public UserController(IUserService userService, IOutputPort outPutPort, WeatherService weatherService)
        {
            _userService = userService;
            _outPutPort = outPutPort;
            _weatherService = weatherService;
        }
        public IActionResult Login()
        {
            return View(); 
        }


        [HttpPost]
        public async Task<IActionResult> Logar( LoginRequestViewModel login)
        {
            try
            {
                var authResult = await _userService.LoginAndGenerateToken(login.Email, login.Password);

                if (authResult == null)
                {
                    ModelState.AddModelError("", "E-mail ou senha inválidos");
                    return View();
                }

                HttpContext.Session.SetString("Token", authResult.Token);
                HttpContext.Session.SetString("UserName", authResult.User.Name);

                ViewBag.UsuarioLogado = true;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return _outPutPort.InvalidRequest();
            }
        }
    }
}
