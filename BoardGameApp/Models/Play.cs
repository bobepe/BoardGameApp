namespace BoardGameApp.Models
{
    public class Play : EntityBase
    {
        public Game Game { get; set; }
        public int GameId { get; set; }
        public DateTime Created { get; set; }
        public List<PlayPlayer> PlayerPlays { get; set; }
    }
}