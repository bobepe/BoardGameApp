using BoardGameApp.Data;
using BoardGameApp.Models;
using BoardGameApp.Models.ViewModels;
using BoardGameApp.Respositories;
using BoardGameApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

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
            viewModel.SelectedPlayerIds = GetIds();
            if (viewModel.SelectedPlayerIds.Count > 0)
                HttpContext.Session.Clear();

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

        [HttpPost]
        public JsonResult AddPlayer(int selectedValue, string selectedText)
        {
            List<int> Ids = GetIds();
            Ids.Add(selectedValue);
            HttpContext.Session.SetString("selectedPlayers", JsonSerializer.Serialize(Ids));

            return Json(new { success = true, message = selectedText });
        }

        [HttpPost]
        public JsonResult RemovePlayer(int selectedValue)
        {
            List<int> Ids = GetIds();
            int index = Ids.IndexOf(selectedValue);
            Ids.RemoveAt(index);
            HttpContext.Session.SetString("selectedPlayers", JsonSerializer.Serialize(Ids));

            return Json(new { success = true, message = "removed" });
        }

        private List<int> GetIds()
        {
            List<int> Ids = new List<int>();
            var serializedResult = HttpContext.Session.GetString("selectedPlayers");
            if (!string.IsNullOrEmpty(serializedResult))
            {
                Ids = JsonSerializer.Deserialize<List<int>>(serializedResult);
            }

            return Ids;
        }
    }
}
