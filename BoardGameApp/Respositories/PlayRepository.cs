using BoardGameApp.Data;
using BoardGameApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGameApp.Respositories
{
    public class PlayRepository : IGenericRepository<Play>
    {
        private readonly BoardGameContext _context;

        public PlayRepository(BoardGameContext context)
        {
            _context = context;
        }

        public IEnumerable<Play> GetAll()
        {
            return _context.Play.ToList();
        }

        public Play GetById(int id)
        {
            return _context.Play
                .FirstOrDefault(x => x.Id == id);
        }

        public Play GetLastPlayByGameId(int gameId)
        {
            return _context.Play
                .OrderByDescending(x => x.Created)
                .FirstOrDefault(x => x.GameId == gameId);
        }
    }
}
