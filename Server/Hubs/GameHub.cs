﻿using BlazorChess.Server.Data;
using BlazorChess.Shared.Models;
using Microsoft.AspNetCore.SignalR;
namespace BlazorChess.Hubs
{
    public class GameHub:Hub
    {
        //public async Task SendMove(string user,MoveOption move)
        //{
        //    await Clients.All.SendAsync("aa",user,move);
        //}

        public async Task SendTest(string user)
        {
            await Clients.All.SendAsync("Test",user);
        }


        public async Task CreateNewGame(ChessContext context)
        {
            Game newGame = new Game();

            context.Games.Add(newGame);
            context.SaveChanges();
            await Clients.All.SendAsync("newGame",newGame);
        }

        public async Task GetGameList(ChessContext context)
        {
           
           var gameList = context.Games.ToList();
           await Clients.Caller.SendAsync("gameList", gameList);
        }
    }
}
