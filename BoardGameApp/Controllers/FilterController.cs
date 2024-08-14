using BoardGameApp.Data;
using BoardGameApp.Models;
using BoardGameApp.Models.ViewModels;
using BoardGameApp.Respositories;
using BoardGameApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameApp.Controllers
{
    public class FilterController : Controller
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly FilterService _filterService;

        public FilterController(IGenericRepository<Game> gameRepository, PlayerRepository playerRepository, FilterService filterService)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _filterService = filterService;
        }

        public async Task<IActionResult> Index()
        {
            var players = _playerRepository.GetAll();
            var games = _gameRepository.GetAll();

            var viewModel = new FilterViewModel
            {
                Players = players.ToList(),
                Games = games.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(FilterViewModel viewModel)
        {
            if (!viewModel.SelectedPlayerIds.Any())
            {
                ModelState.AddModelError(string.Empty, "You need to select at least one player.");
                var players = _playerRepository.GetAll();
                var games = _gameRepository.GetAll();

                viewModel.Players = players.ToList();
                viewModel.Games = games.ToList();

                return View("Index", viewModel);
            }

            var result = _filterService.GetFilteredGameDetails(viewModel.SelectedPlayerIds, viewModel.SelectedGameId);

            return View("FilterResults", result);
        }
    }
}
