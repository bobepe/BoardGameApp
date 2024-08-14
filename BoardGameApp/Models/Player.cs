namespace BoardGameApp.Models
{
    public class Player : EntityBase
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsNonPlayer { get; set; }
        public List<PlayPlayer> PlayerPlays { get; set; }
    }
}