﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorChess.Shared.Models
{
    public class Game
    {
        public int Id { get; set; }
        [NotMapped]
        public Board GameBoard { get; set; }
        public bool IsBlackTurn { get; set; }
        public virtual List<HistoryMove> MovesMade { get; set; } = new List<HistoryMove>();
        [NotMapped]
        public int MoveToShow { get; set; }

        public event Action OnChange;

        public Game()
        {
            SetBaseState();
        }

        public void SetBaseState()
        {
            GameBoard = new Board();
            IsBlackTurn = false;
            MoveToShow = -1;
        }

        public bool Move(int fromX, int fromY, int toX, int toY)
        {
            var toMove = GameBoard.Tiles[fromX, fromY].OccupyingPrice;

            if (toMove.IsBlack != IsBlackTurn)
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
        public bool Move(HistoryMove move)
        {
            return Move(move.FromX, move.FromY, move.ToX, move.ToY);
        }

        public void NotifyStateChanged() => OnChange?.Invoke();



    }
}