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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


/*TODO: 

    fixa runda kanter på boarden
    minimax / when 2 winning rows in next step, doesnt block either.
    fixa footer - about page instead of privacy?
    rotate image instead of td on winning row. make it 180 deg instead 
    bestäm vem som börjar (kanske)
    Gör något för how to play
    Fixa mobil
    docker
    jenkins
    terraform azure
    Storlekar på mobil spelplan och ev. annat
*/
