using BoardGameApp.Models;
using BoardGameApp.Models.ViewModels;
using BoardGameApp.Respositories;

namespace BoardGameApp.Services
{
    public class GameService
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly PlayRepository _playRepository;
        private readonly PlayPlayerRepository _playPlayerRepository;

        public GameService(IGenericRepository<Game> gameRepository, PlayRepository playRepository, PlayPlayerRepository playPlayerRepository)
        {
            _gameRepository = gameRepository;
            _playRepository = playRepository;
            _playPlayerRepository = playPlayerRepository;
        }

        public IEnumerable<Game> GetGames()
        {
            IEnumerable<Game> games = _gameRepository.GetAll();
            return games.OrderBy(x => x.Name).ToList();
        }

        public Game GetGameById(int id)
        {
            return _gameRepository.GetById(id);
        }

        public Player GetPlayerWithMostWins(int gameId)
        {
            // Získání všech PlayPlayer záznamů pro danou hru
            var playPlayers = _playPlayerRepository.GetPlayPlayersByGameId(gameId);

            // Skupinování podle PlayerId a určení počtu vítězství
            var playerWins = playPlayers
                .GroupBy(pp => pp.PlayerId)
                .Select(group => new
                {
                    PlayerId = group.Key,
                    Wins = group.Count(pp => pp.Score == group.Max(g => g.Score))
                })
                .OrderByDescending(p => p.Wins)
                .FirstOrDefault();

            // Pokud je nalezen hráč s vítězstvími
            if (playerWins != null)
            {
                // Získání detailních informací o hráči
                var player = playPlayers
                    .Select(pp => pp.Player)
                    .FirstOrDefault(p => p.Id == playerWins.PlayerId);

                return player;
            }

            return null;
        }

        public GameDetailsViewModel GetGameDetails(int gameId)
        {
            // Získání detailů hry
            var game = _gameRepository.GetById(gameId);
            if (game == null)
            {
                return null;
            }

            // Získání všech PlayPlayer záznamů pro danou hru
            var playPlayers = _playPlayerRepository.GetPlayPlayersByGameId(gameId);
            Play lastPlay = _playRepository.GetLastPlayByGameId(gameId);
            Player bestScorePlayer = null;
            double bestScore = 0;
            Role bestScoreRole = null;
            Dictionary<Player, int> PlayerWithWins = new Dictionary<Player, int>();
            foreach (var item in playPlayers)
            {
                if (bestScore <  item.Score)
                {
                    bestScore = item.Score;
                    bestScorePlayer = item.Player;
                    bestScoreRole = item.Role;
                }

                if (item.Position == 1)
                {
                    if (PlayerWithWins.ContainsKey(item.Player))
                        PlayerWithWins[item.Player] += 1;
                    else
                        PlayerWithWins.Add(item.Player, 1);
                }
            }

            List<Tuple<Player, double, Role>> lastplayplayers = new List<Tuple<Player, double, Role>>();
            var l = playPlayers.Where(i => i.PlayId == lastPlay.Id);
            foreach (var item in l)
            {
                lastplayplayers.Add(new Tuple<Player, double, Role>(item.Player, item.Score, item.Role));
            }

            return new GameDetailsViewModel()
            {
                Game = game,
                PlayerWithWins = PlayerWithWins,
                BestScore = new Tuple<Player, double, Role>(bestScorePlayer, bestScore, bestScoreRole),
                LastPlay = lastPlay,
                LastPlayPlayers = lastplayplayers
            };
        }
    }
}
