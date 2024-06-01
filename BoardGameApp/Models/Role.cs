namespace BoardGameApp.Models
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public List<PlayPlayer> PlayerPlays { get; set; }
    }
}
