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
    public class PlayController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public PlayController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult AgainstComputer()
        {
            return View();
        }

        public IActionResult AgainstPlayer()
        {
            return View();
        }
    }
}
