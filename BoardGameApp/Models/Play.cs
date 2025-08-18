namespace BoardGameApp.Models
{
    public class Play : EntityBase
    {
        public Game Game { get; set; }
        public int GameId { get; set; }
        public DateTime Created { get; set; }
        public bool IsSolo { get; set; }
        public bool IsOnline { get; set; }
        public int Time { get; set; }
        public List<PlayPlayer> PlayerPlays { get; set; }
    }
}