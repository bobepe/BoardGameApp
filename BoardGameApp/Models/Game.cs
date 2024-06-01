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
        public List<Play> Plays { get; set; }
        public List<Role> Roles { get; set; }
    }
}