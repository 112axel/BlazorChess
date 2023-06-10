using BlazorChess.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorChess.Data
{
    public class ChessContext:DbContext
    {
        public DbSet<Game> Games { get; set; }

        public ChessContext(DbContextOptions<ChessContext> options) : base(options) { }

    }
}
