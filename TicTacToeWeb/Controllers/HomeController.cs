using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TicTacToeWeb.Models;

namespace TicTacToeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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


/*TODO: 

    fixa runda kanter på boarden
    2 players signalR https://github.com/splttingatms/TicTacToe
    minimax / when 2 winning rows in next step, doesnt block either.
    fixa header/footer
    rotate image instead of td on winning row. make it 180 deg instead 
    refactor animateWinningRow to be called from if (checkWin()){}
    bestäm vem som börjar (kanske)
    Fixa mobil
    docker
    jenkins
    terraform azure
    Storlekar på mobil spelplan och ev. annat
*/
