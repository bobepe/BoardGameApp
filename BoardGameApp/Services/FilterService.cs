using BoardGameApp.Models;
using BoardGameApp.Models.ViewModels;
using BoardGameApp.Respositories;

namespace BoardGameApp.Services
{
    public class FilterService
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly PlayRepository _playRepository;
        private readonly PlayPlayerRepository _playPlayerRepository;

        public FilterService(IGenericRepository<Game> gameRepository, PlayRepository playRepository, PlayPlayerRepository playPlayerRepository)
        {
            _gameRepository = gameRepository;
            _playRepository = playRepository;
            _playPlayerRepository = playPlayerRepository;
        }

        public List<FilterDetailsViewModel> GetFilteredGameDetails(List<int> playerIds, int? gameId)
        {
            var games = _gameRepository.GetAll();

            if (gameId.HasValue)
            {
                games = games.Where(g => g.Id == gameId.Value).ToList();
            }

            List<FilterDetailsViewModel> results = new List<FilterDetailsViewModel>();          
            foreach (var game in games)
            {
                var viewModel = new FilterDetailsViewModel();
                List<PlayPlayer> playsCount = GetPlaysCount(playerIds, game.Id);

                // Set results in the ViewModel
                viewModel.Game = game;
                viewModel.Count = playsCount.Select(pp => pp.PlayId).Distinct().Count();
                viewModel.Records = GetRecords(playsCount);
                if (viewModel.Records.Count == 0) continue;
                viewModel.Winners = GetWinners(playsCount);
                viewModel.TopRoles = GetTopRoles(playsCount);

                results.Add(viewModel);
            }

            return results;
        }

        private List<PlayPlayer> GetPlaysCount(List<int> playerIds, int gameId)
        {
            var playPlayers = _playPlayerRepository.GetPlayPlayersByGameId(gameId);

            var filteredPlayPlayers = playPlayers
                .GroupBy(pp => pp.PlayId)
                .Where(group => group.All(pp => playerIds.Contains(pp.PlayerId)) && group.Count() == playerIds.Count)
                .SelectMany(group => group)
                .ToList();

            return filteredPlayPlayers;
        }

        private List<Tuple<Player, double, Role>> GetRecords(List<PlayPlayer> playPlayers)
        {
            var maxScorePlayer = playPlayers
                .OrderByDescending(pp => pp.Score)
                .FirstOrDefault();

            if (maxScorePlayer == null)
            {
                return new List<Tuple<Player, double, Role>>();
            }

            return new List<Tuple<Player, double, Role>>
            {
                Tuple.Create(maxScorePlayer.Player, maxScorePlayer.Score, maxScorePlayer.Role)
            };
        }

        private List<Tuple<Player, double>> GetWinners(List<PlayPlayer> playPlayers)
        {
            var winners = playPlayers
               .Where(pp => pp.Position == 1)
               .GroupBy(pp => pp.Player)
               .Select(group => new
               {
                   Player = group.Key,
                   WinCount = group.Count()
               })
               .OrderByDescending(x => x.WinCount)
               .Select(x => Tuple.Create(x.Player, (double)x.WinCount))
               .ToList();

            return winners;
        }

        private Dictionary<Player, List<Tuple<Role, int, double>>> GetTopRoles(List<PlayPlayer> playPlayers)
        {
            var topRoles = playPlayers
                .GroupBy(pp => new { pp.Player, pp.Role })
                .Select(group => new
                {
                    Player = group.Key.Player,
                    Role = group.Key.Role,
                    GameCount = group.Count(), // Počet her za tuto roli
                    WinCount = group.Count(pp => pp.Position == 1) // Počet výher za tuto roli
                })
                .OrderByDescending(x => x.GameCount)
                .GroupBy(x => x.Player)
                .ToDictionary(
                    g => g.Key, // Klíčem je hráč
                    g => g
                        .OrderByDescending(x => x.GameCount) // Seřazení podle počtu her sestupně
                        .Select(x => Tuple.Create(x.Role, x.GameCount, (double)x.WinCount))
                        .ToList() // Hodnota je seznam Tuple pro každého hráče
                );

            if (!ContainsNonNullRole(topRoles)) topRoles.Clear();

            return topRoles;
        }

        private bool ContainsNonNullRole(Dictionary<Player, List<Tuple<Role, int, double>>> playerRolesDict)
        {
            foreach (var playerRoles in playerRolesDict.Values)
            {
                if (playerRoles.Any(tuple => tuple.Item1 != null))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
