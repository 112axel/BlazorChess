using BlazorChess.Shared.Models.Pieces;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public bool IsFlipped { get; set; }

        public event Action? OnChange;
        public event Action? OnSmallChange;

        public Game()
        {
            GameBoard = new Board();
            IsBlackTurn = false;
        }

        public void SetMoveToShowToCurrent()
        {
            MoveToShow = MovesMade.Count;
        }

        public void SetBaseState()
        {
            GameBoard = new Board();
            IsBlackTurn = false;
            
        }

        public bool Move(int fromX, int fromY, int toX, int toY, PromotionChoise promotionChoise)
        {
            var toMove = GameBoard.Tiles[fromX, fromY].OccupyingPiece;

            if(toMove == null)
            {
                return false;
            }

            if (toMove.IsBlack != IsBlackTurn)
            {
                return false;
            }

            bool allowedMove = GameBoard.Move(fromX, fromY, toX, toY, promotionChoise);
            if (allowedMove)
            {
                IsBlackTurn = !IsBlackTurn;
            }

            return allowedMove;

        }

        public bool MoveRequiresPremotion(int fromX, int fromY, int toX, int toY)
        {
            var tocheck = GameBoard.Tiles[fromX, fromY].OccupyingPiece;
            if(tocheck is Pawn)
            {
                int promotionY = tocheck.IsBlack ? 7:0;
                if(toY == promotionY)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Move(HistoryMove move)
        {
            return Move(move.FromX, move.FromY, move.ToX, move.ToY,move.PromotionChoise);
        }


        public void SetGameStateToTurn(int turn)
        {
            SetBaseState();


            List<HistoryMove> movesToMake = MovesMade;
            if (turn > -1)
            {
                movesToMake = MovesMade.Take(turn).ToList();
            }

            foreach (var move in movesToMake)
            {
                Move(move);
            }

            MoveToShow = turn;
        }

        public void NotifyStateChanged() => OnChange?.Invoke();
        public void NotifySmallChange() => OnSmallChange?.Invoke();



    }
}
