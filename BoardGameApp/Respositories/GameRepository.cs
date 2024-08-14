using BoardGameApp.Data;
using BoardGameApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGameApp.Respositories
{
    public class GameRepository : IGenericRepository<Game>
    {
        private readonly BoardGameContext _context;

        public GameRepository(BoardGameContext context)
        {
            _context = context;
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Game.ToList();
        }

        public Game GetById(int id)
        {
            return _context.Game
                .Include(x => x.Plays)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
