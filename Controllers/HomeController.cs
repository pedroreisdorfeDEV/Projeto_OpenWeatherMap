using Microsoft.AspNetCore.Mvc;
using projetoGloboClima.Models;
using projetoGloboClima.Services.Interfaces;
using projetoGloboClima.Shared.OutPut;
using System.Diagnostics;

namespace projetoGloboClima.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IOutputPort _outPutPort;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IOutputPort outPutPort)
        {
            _logger = logger;
            _userService = userService;
            _outPutPort = outPutPort;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
