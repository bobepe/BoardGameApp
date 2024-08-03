using BoardGameApp.Data;
using BoardGameApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGameApp.Respositories
{
    public class PlayPlayerRepository
    {
        private readonly BoardGameContext _context;

        public PlayPlayerRepository(BoardGameContext context)
        {
            _context = context;
        }

        public List<PlayPlayer> GetPlayPlayersByGameId(int gameId)
        {
            return _context.PlayPlayer
                .Include(x => x.Player)
                .Include(x => x.Role)
                .Include(x => x.Play)
                .Where(pp => pp.Play.GameId == gameId)
                .ToList();
        }
    }
}
