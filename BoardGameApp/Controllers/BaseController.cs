using BoardGameApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameApp.Controllers
{
    public class BaseController : Controller
    {
        protected BoardGameContext _context { get; set; }
        public BaseController(BoardGameContext context)
        {
            _context = context;
        }
    }
}
