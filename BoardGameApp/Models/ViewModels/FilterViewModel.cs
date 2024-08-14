namespace BoardGameApp.Models.ViewModels
{
    public class FilterViewModel
    {
        public List<Player> Players { get; set; }
        public List<Game> Games { get; set; }
        public List<int> SelectedPlayerIds { get; set; } = new List<int>();
        public int? SelectedGameId { get; set; }
    }
}
