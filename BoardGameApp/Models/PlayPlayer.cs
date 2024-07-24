namespace BoardGameApp.Models
{
    public class PlayPlayer : EntityBase
    {
        public int PlayId { get; set; }
        public Play Play { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public double Score { get; set; }
        public int Position { get; set; }
    }
}