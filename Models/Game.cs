﻿namespace BlazorChess.Models
{
    public class Game
    {
        public Board GameBoard { get; set; }
        public bool IsBlackTurn { get; set; }

        public Game()
        {
            GameBoard = new Board();
            IsBlackTurn = false;
        }

        public bool Move(int fromX, int fromY, int toX, int toY)
        {
            var toMove = GameBoard.Tiles[fromX, fromY].OccupyingPrice;

            if(toMove.IsBlack != IsBlackTurn)
            {
                return false;
            }
            
          bool allowedMove = GameBoard.Move(fromX, fromY, toX, toY);
            if (allowedMove)
            {
                IsBlackTurn = !IsBlackTurn;
            }

            return allowedMove;

        }


        
    }
}
