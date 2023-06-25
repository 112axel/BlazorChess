using BlazorChess.Shared.Models;
using BlazorChess.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorChess.Server.Data
{
    public class ChessContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        public ChessContext(DbContextOptions<ChessContext> options) : base(options) { }

    }
}
