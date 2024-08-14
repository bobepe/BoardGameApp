namespace BoardGameApp.Models.ViewModels
{
    public class GameDetailsViewModel
    {
        public Game Game { get; set; }
        public Dictionary<Player, int> PlayerWithWins { get; set; }
        public Tuple<Player, double, Role> BestScore { get; set; }
        public Play LastPlay { get; set; }
        public List<Tuple<Player,  double, Role>> LastPlayPlayers { get; set; }
    }
}
