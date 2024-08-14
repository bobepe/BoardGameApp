namespace BoardGameApp.Models.ViewModels
{
    public class FilterDetailsViewModel
    {
        public Game Game { get; set; }
        public int Count { get; set; }
        public List<Tuple<Player, double, Role>> Records { get; set; }
        public List<Tuple<Player, double, Role>> AllRecords { get; set; }
        public List<Tuple<Player, double>> Winners { get; set; }
        public Dictionary<Player, List<Tuple<Role, int, double>>> TopRoles { get; set; }
    }
}
