using BlazorChess.Server.Data;
using BlazorChess.Shared.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlazorChess.Hubs
{
    //TODO strongly type
    public interface IBoradHub
    {
        Task FullLoad(List<HistoryMove> movesMade);
    }

    public class BoardHub:Hub
    {
        private readonly ChessContext dbContext;
        public BoardHub(ChessContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //TODO serverside check
        public async Task GetHistory(int id)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
            //TODO remove frist?
            var historyMoves = dbContext.Games.Include(x=>x.MovesMade).First(x=>x.Id == id).MovesMade.ToList();
     
            await Clients.Caller.SendAsync("fullLoad", historyMoves);
        }
        public async Task MakeMove(int id,HistoryMove move)
        {
            dbContext.Games.Include(x => x.MovesMade).First(x => x.Id == id).MovesMade.Add(move);
            dbContext.SaveChanges();
            //TODO not resend all data
            var historyMoves = dbContext.Games.Include(x=>x.MovesMade).First(x=>x.Id == id).MovesMade.ToList();
            //await Clients.Group(id.ToString()).SendAsync("move", move);
            await Clients.Group(id.ToString()).SendAsync("move", move);
        }
    }
}
