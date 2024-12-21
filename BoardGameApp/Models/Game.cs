using System.Security.AccessControl;

namespace BoardGameApp.Models
{
    public class Game : EntityBase
    {
        public string Name { get; set; }
        public bool NoPoints { get; set; }
        public bool HighestScoreWins { get; set; }
        public bool Cooperative { get; set; }
        public bool PlayInTeamsByDefault { get; set; }
        public bool IsMine { get; set; }
        public GameType GameType { get; set; }
        public List<Play> Plays { get; set; }
        public List<Role> Roles { get; set; }
    }

    public enum GameType
    {
        Normal,
        Party,
        Campaign
    }
}