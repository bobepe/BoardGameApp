using BoardGameApp.Models;
using BoardGameApp.Models.ViewModels;
using BoardGameApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameApp.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var games = _gameService.GetGames();
            return View(games);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _gameService.GetGameDetails(id.Value);

            if (viewModel.Game == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
    }
}
