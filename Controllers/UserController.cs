using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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


        /// <summary>
        /// Faz login do usuário e gera um token JWT.
        /// </summary>
        [HttpPost("api/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogarApi([FromBody] LoginRequestViewModel login)
        {
            var authResult = await _userService.LoginAndGenerateToken(login.Email, login.Password);

            if (authResult == null)
                return BadRequest(new { message = "E-mail ou senha inválidos!" });

            return Ok(new { token = authResult.Token, user = authResult.User });
        }

        [HttpPost]
        public async Task<IActionResult> Logar(LoginRequestViewModel login)
        {
            try
            {
                var authResult = await _userService.LoginAndGenerateToken(login.Email, login.Password);

                if (authResult == null)
                {
                    ViewBag.PopupMensagem = "E-mail ou senha inválidos!";
                    return View("Login");
                }

                HttpContext.Response.Cookies.Append("jwtToken", authResult.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // true em produção
                    SameSite = SameSiteMode.Strict
                });

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


        /// <summary>
        /// Adiciona uma cidade favorita para o usuário logado.
        /// </summary>
        [Authorize]
        [HttpPost("api/favorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> FavoriteApi([FromBody] WeatherViewModel model)
        {
            var usuarioId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized();

            bool sucesso = await _userService.AddFavoriteCity(usuarioId, model);

            if (sucesso)
                return Ok(new { message = "Cidade favoritada com sucesso!" });

            return BadRequest(new { message = "Não foi possível favoritar a cidade." });
        }

        [Authorize] ///!!!Somente usuários autenticados
        [HttpPost]
        public async Task<IActionResult> Favorite(WeatherViewModel model)
        {
            var usuarioId = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized();

            bool sucesso = await _userService.AddFavoriteCity(usuarioId, model);

            if (sucesso)
                return RedirectToAction("IndexWeather", "Weather");

            return BadRequest("Não foi possível favoritar a cidade.");
        }


        /// <summary>
        /// Faz logout limpando a sessão.
        /// </summary>
        [HttpPost("api/logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult LogoutApi()
        {
            HttpContext.Session.Clear();
            return Ok(new { message = "Logout realizado com sucesso!" });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Home"); 
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            try
            {
                var user = new UserEntity
                {
                    UserId = Guid.NewGuid().ToString(),
                    User = model.User,
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                };


                bool retorno = await _userService.CreateUser(user);
                if (retorno)
                {
                    ViewBag.PopupMensagem = "Usuário cadastrado com sucesso!\nAgora você pode fazer login.";
                }
                else
                {
                    ViewBag.PopupMensagem = $"Erro ao cadastrar: {user.Name}";
                }      
            }
            catch (Exception ex)
            {
                ViewBag.PopupMensagem = $"Erro ao cadastrar: {model.Name}";
            }

            return View("Login"); 
        }


        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        [HttpPost("api/register")]
        [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterApi([FromBody] UserViewModel model)
        {
            try
            {
                var user = new UserEntity
                {
                    UserId = Guid.NewGuid().ToString(),
                    User = model.User,
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                };

                bool retorno = await _userService.CreateUser(user);
                if (retorno)
                    return Ok(new { message = "Usuário cadastrado com sucesso!", user });

                return BadRequest(new { message = "Erro ao cadastrar usuário." });
            }
            catch
            {
                return BadRequest(new { message = "Erro inesperado." });
            }
        }

        public IActionResult CreateUser()
        {
            return View();
        }



    }
}
