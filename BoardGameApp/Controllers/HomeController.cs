using BoardGameApp.Data;
using BoardGameApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BoardGameApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(BoardGameContext context, ILogger<HomeController> logger) : base(context)
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

        public IActionResult Users()
        {
            ViewBag.Name = "test";
            List<Person> people = _context.Persons.ToList();
            ViewBag.People = people;


            List<string> playerNames = new List<string> { "M�a", "Pe�a", "Ko��k" }; // Seznam jmen hr���
            string gameName = "Duna"; // N�zev hry

            var gamesForPlayers = _context.Play
                .Include(p => p.PlayerPlays)
                    .ThenInclude(pp => pp.Player)
                .Include(p => p.Game)
                .Where(play =>
                    play.Game.Name == gameName &&
                    play.PlayerPlays.Count(pp => playerNames.Contains(pp.Player.Name)) == playerNames.Count)
                .AsEnumerable() // P�echod na hodnocen� na stran� klienta
                .Select(play => new
                {
                    Game = play.Game.Name,
                    PlayerScores = play.PlayerPlays
                        .Where(pp => playerNames.Contains(pp.Player.Name))
                        .ToDictionary(pp => pp.Player.Name, pp => pp.Score) // Dictionary pro sk�re hr���
                });

            Console.WriteLine($"Statistika pro hru {gameName}:");

            foreach (var game in gamesForPlayers)
            {
                Console.WriteLine($"Hra: {game.Game}");

                foreach (var playerScore in game.PlayerScores)
                {
                    Console.WriteLine($"Hr��: {playerScore.Key}, Sk�re: {playerScore.Value}");
                }
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
