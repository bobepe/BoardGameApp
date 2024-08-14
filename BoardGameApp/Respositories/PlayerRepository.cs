using BoardGameApp.Data;
using BoardGameApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGameApp.Respositories
{
    public class PlayerRepository : IGenericRepository<Player>
    {
        private readonly BoardGameContext _context;

        public PlayerRepository(BoardGameContext context)
        {
            _context = context;
        }

        public IEnumerable<Player> GetAll()
        {
            return _context.Player.ToList();
        }

        public Player GetById(int id)
        {
            return _context.Player
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
