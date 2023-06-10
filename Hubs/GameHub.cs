using BlazorChess.Models;
using Microsoft.AspNetCore.SignalR;
namespace BlazorChess.Hubs
{
    public class GameHub:Hub
    {
        public async Task SendMove(string user,MoveOption move)
        {
            await Clients.All.SendAsync("aa",user,move);
        }
    }
}
