using BlazorChess.Shared.Models;

namespace BlazorChess.Shared.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(bool isBlack) : base("bishop", isBlack)
        {
        }

        public override List<MoveOption> AllowedMoves(Board board, int x, int y)
        {
            return DiagonalMove(board, x, y);
        }
    }
}
